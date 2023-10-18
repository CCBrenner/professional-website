using System.Timers;

namespace ProfessionalWebsite.Client.Services.SudokuService;

public class BruteForceSolver : ISolver
{
    private BruteForceSolver()
    {
        RemainingCells = new();
        PreviousCells = new();

        SolveHasStarted = false;
        PuzzleIsSolvable = true;
        StopwatchTime = 0;
        progressValue = 0;

        Timer = new(100);  // every tenth of a second
        SetupTimer();
    }

    private Puzzle? _puzzle;
    private int progressValue;
    public System.Timers.Timer Timer { get; private set; }
    private Cell? CurrentCell;
    public Stack<Cell> RemainingCells { get; private set; }
    public Stack<Cell> PreviousCells { get; private set; }
    public bool SolveHasStarted { get; private set; }
    public bool PuzzleIsSolvable { get; private set; }
    public decimal StopwatchTime { get; private set; }
    public int ProgressPercentage => progressValue / 81 * progressValue / 81 * 100;
    public event Action<string>? OnSolverLoopIteration;

    public static BruteForceSolver Create() => new();
    public void RaiseEventOnSolverLoopIteration()
    {
        if (OnSolverLoopIteration != null)
        {
            OnSolverLoopIteration.Invoke("meaningless");
        }
    }

    public bool Solve(Puzzle puzzle)
    {
        // Config based on Puzzle:

        _puzzle = puzzle;
        _puzzle.Cells.Reverse();  // reversed (for creating RemainingCells stack)
        foreach (var cell in _puzzle.Cells)
        {
            if (cell.ValueStatus != ValueStatus.Given && cell.ValueStatus != ValueStatus.Confirmed)
            {
                RemainingCells.Push(cell);
            }
        }
        _puzzle.Cells.Reverse();  // reversed back to original order

        CurrentCell = RemainingCells.Pop();

        // End config.

        int candidate;

        int counter = 1;

        SolveHasStarted = true;

        MarkCellsWithNonZeroValuesAsGiven();

        //ConsoleRender.RenderMatrixWithMetaData(Puzzle);

        //Timer.Start();

        _puzzle.RemoveCandidates();

        while (true)
        {
            // get the next candidate to try
            candidate = CurrentCell.GetNextCandidate();

            // if CurrentCell is the CellId 1 and there are no other candidates to try, exit
            if (candidate == 0 && PreviousCells.Count == 0)
            {
                PuzzleIsSolvable = false;
                break;
            }

            // if there are no other candidates to try, then backtrack to previous cell
            if (candidate == 0)
            {
                // Use only one of the following at a single time:
                //GoBackToLastCellWithUntriedCandidatesIterative();
                GoBackToLastCellWithUntriedCandidatesRecursive();  // <= Optimize with F# (somehow)
                continue;
            }

            // assign return value as value of current cell 
            CurrentCell.SetExpectedValue(candidate);
            if (RemainingCells.Count == 0) break;

            // also assign same return value to TriedCandidates stack in Cell api
            CurrentCell.AddTriedCandidate(candidate);

            // since Value becomes free, add that value to all respective cells that would have it as a candidate
            // and then eliminate all non possible candidates
            _puzzle.UpdateCandidates();

            // assign CurrentCell to PreviousCells stack in Puzzle api
            GoToNextCell();

            //RaiseEventOnSolverLoopIteration();

            if (counter % 100 == 0) Console.WriteLine(counter);

            counter++;
        }

        //Timer.Stop();

        Console.WriteLine(counter);

        // return true if Puzzle is solved; false if could not be solved
        return PuzzleIsSolvable;
    }

    private void MarkCellsWithNonZeroValuesAsGiven()
    {
        foreach (Cell cell in _puzzle.Cells)
        {
            if (cell.Value != 0)
            {
                cell.SetValueStatusToGiven();
            }
        }
    }

    private void SetupTimer()
    {
        Timer.Elapsed += TimerElapsed;
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        StopwatchTime += 0.1M;
        Console.WriteLine(StopwatchTime.ToString());
    }

    private void GoBackToLastCellWithUntriedCandidatesIterative()
    {
        int candidate;

        while (true)
        {
            // Set Cell & Record Txn before leaving cell:
            int previousValue = CurrentCell.Value;

            CurrentCell.ResetTriedCandidates();
            CurrentCell.ResetValue();

            if (previousValue != CurrentCell.Value)
            {
                _puzzle.Ledger.RecordNewTxn(CurrentCell.Id, 0, previousValue, CurrentCell.Value);
            }

            // Move:
            MoveToPreviousCell();

            // Update available candidates, particularly adding candidates to the cell we just left:
            _puzzle.UpdateCandidates();

            // Get the next candidate to try for the CurrentCell:
            candidate = CurrentCell.GetNextCandidate();

            // If all candidates of all potential cells have been tried, then Puzzle is not solvable. Exit.
            if (candidate == 0 && PreviousCells.Count == 0)
            {
                PuzzleIsSolvable = false;
                return;
            }

            // If there are no other candidates to try for CurrentCell, then restart this loop.
            if (candidate != 0)
            {
                return;
            }
        }
    }
    private int GoBackToLastCellWithUntriedCandidatesRecursive()
    {
        // when a cell has no more candidates to try when calling GetNextCandidate:
        // clear the TriedCandidates stack of CurrentCell until stack count == 0
        int previousValue = CurrentCell.Value;

        CurrentCell.ResetTriedCandidates();
        CurrentCell.ResetValue();

        if (previousValue != CurrentCell.Value)
        {
            _puzzle.Ledger.RecordNewTxn(CurrentCell.Id, 0, previousValue, CurrentCell.Value);
        }

        // push cell unto RemainingCells stack && Pop cell from PreviousCells stack && Assign popped cell from stack to CurrentCell property
        MoveToPreviousCell();

        // since Value becomes free, add that value to all respective cells that would have it as a candidate
        // and then eliminate all non possible candidates
        _puzzle.UpdateCandidates();

        // get the next candidate to try
        int candidate = CurrentCell.GetNextCandidate();

        // if CurrentCell is the CellId 1 and htere are no other candidates to try, exit
        if (candidate == 0 && PreviousCells.Count == 0)
        {
            PuzzleIsSolvable = false;
            return candidate;
        }

        // if there are no other candidates to try, then backtrack to previous cell
        if (candidate == 0)
        {
            candidate = GoBackToLastCellWithUntriedCandidatesRecursive();
        }

        return candidate;
    }
    private void GoToNextCell()
    {
        PreviousCells.Push(CurrentCell);
        CurrentCell = RemainingCells.Pop();
        UpdateProgressBarValueIfNewestCell(CurrentCell);
    }

    private void UpdateProgressBarValueIfNewestCell(Cell currentCell)
    {
        // definition of newest:
        if (CurrentCell.Id > ProgressPercentage)
        {
            progressValue = CurrentCell.Id;
        }
    }

    private void MoveToPreviousCell()
    {
        RemainingCells.Push(CurrentCell);
        CurrentCell = PreviousCells.Pop();
    }
}