using System.Collections.Generic;
using System.Timers;

namespace ProfessionalWebsite.Client.Services.SudokuService;
/*
public class BruteForceSolverScratch : ISolver
{
    private BruteForceSolverScratch()
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

    public static BruteForceSolverScratch Create() => new();
    public void RaiseEventOnSolverLoopIteration()
    {
        if (OnSolverLoopIteration != null)
        {
            OnSolverLoopIteration.Invoke("meaningless");
        }
    }
    public bool Solve(Puzzle puzzle)
    {
        List<int> GetCandidates(Cell cell)
        {
            SortedSet<int> sortedSet = new();

            for (int i = 1; i< cell.Values.Count(); i++)
            {
                sortedSet.Add(cell.Values[i]);
            }

            List<int> result = sortedSet.ToList();
            if (result[0] == 0) result.RemoveAt(0);

            return result;
        }

        int GetNextCandidate(List<int> candidates) =>
            candidates
            .Where(x => !CurrentCell.TriedCandidates.Contains(x))
            .Select(x => x)
            .FirstOrDefault(Cell.NonPossibilityPlaceholderValue);

        List<List<Cell>> GoToNextCell(List<List<Cell>> solveCells)
        {
            // means puzzle has bene solved (because no more remaining cells)
            // this check should be moved outside and in front of this function
            //if (solveCells[2].Count == 0) return solveCells;

            // assume there is a current cell
            // assume there is another remaining cell

            // newPrevious:
            Cell currentMovingToPrev = solveCells[1][0];
            List<Cell> newPrev = new(solveCells[0]);
            newPrev.Insert(0, currentMovingToPrev);

            // newCurrent:
            Cell remainingMovingToCurrent = solveCells[2][0];
            List<Cell> newCurrent = new() { remainingMovingToCurrent };

            // newRemaining:
            List<Cell> newRemaining = solveCells[2]
                .Where(x => x != remainingMovingToCurrent)
                .Select(x => x)
                .ToList();

            return new()
            {
                newPrev,
                newCurrent,
                newRemaining
            };
        }

        // Setup based on Puzzle:
        _puzzle = puzzle;
        _puzzle.Cells.Reverse();
        Stack<Cell> remainingCells = new();
        foreach (var cell in _puzzle.Cells)
        {
            if (cell.ValueStatus != ValueStatus.Given && cell.ValueStatus != ValueStatus.Confirmed)
            {
                remainingCells.Push(cell);
            }
        }
        _puzzle.Cells.Reverse();
        Cell starterCurrentCell = remainingCells.Pop();
        List<List<Cell>> solveCells = new List<List<Cell>>()
        {
            new() { },
            new() { starterCurrentCell },
            remainingCells.ToList()
        };

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

        // End setup.

        int candidate;

        int counter = 1;

        SolveHasStarted = true;

        MarkCellsWithNonZeroValuesAsGiven();

        //GetPuzzleAsString.RenderMatrixWithMetaData(Puzzle);

        //Timer.Start();

        _puzzle.RemoveCandidates();

        while (true)
        {
            // get the next candidate to try
            candidate = CurrentCell.GetNextCandidate();
            List<int> candidates = GetCandidates(CurrentCell);
            /*
            List<int> vals = new();
            foreach (var val in candidates)
            {
                if (!CurrentCell.TriedCandidates.Contains(val))
                {
                    vals.Add(val);
                }
            }
            candidate = vals.Count > 0 ? vals[0] : Cell.NonPossibilityPlaceholderValue;
            *
            /*
            candidate =
                GetCandidates(CurrentCell)
                .Where(x => !CurrentCell.TriedCandidates.Contains(x))
                .Select(x => x)
                .FirstOrDefault(Cell.NonPossibilityPlaceholderValue);
            *
            candidate = GetNextCandidate(candidates);

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

            // rehydrate candidates for unconfirmed cells and perform elimination based on new value assignment
            _puzzle.UpdateCandidates();

            // assign CurrentCell to PreviousCells stack in Puzzle api
            GoToNextCell(solveCells);

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

            // V1_Move:
            MoveToPreviousCell();

            // Update available candidates, particularly adding candidates to the cell we just left:
            _puzzle.UpdateCandidates();

            // Get the next candidate to try for the CurrentCell:
            int candidate = CurrentCell.GetNextCandidate();

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
    private void GoBackToLastCellWithUntriedCandidatesRecursive()
    {
        //int GoBackToLastCellWithUntriedCandidatesRecursiveLocal()
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
            return;
        }

        // if there are no other candidates to try, then backtrack to previous cell
        if (candidate == 0)
        {
            GoBackToLastCellWithUntriedCandidatesRecursive();
        }

        return;
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
*/