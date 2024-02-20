namespace ProfessionalWebsite.Client.Classes.SudokuSolverProject;

public sealed class Sudoku : ISudoku
{
    private const string DEFAULT_PUZZLE = "Custom";
    private Sudoku(string defaultPuzzle)
    {
        Reset(defaultPuzzle);
    }

    private static Sudoku? api = null;
    private static readonly object lockobject = new object();
    public static Sudoku Api
    {
        get
        {
            lock (lockobject)
            {
                if (api is null)
                    api = Create();
                return api;
            }
        }
    }

    private static Sudoku? Create() => new(DEFAULT_PUZZLE);
    private void Reset(string puzzleBookSelection)
    {
        _selectedMatrix = puzzleBookSelection;
        ConsoleVersion = "gui";
        Puzzle = SudokuSolverProject.Puzzle.CreateWithBruteForceSolver();

        if (_selectedMatrix != DEFAULT_PUZZLE)
        {
            int[,] matrixToLoad = PuzzleBook.GetPuzzle(_selectedMatrix);
            Puzzle.LoadMatrixAsCellValues(matrixToLoad);
        }
        
        Puzzles = PuzzleBook.GetPuzzles();
        LocalConsole = string.Empty;
        IsSolved = null;
    }
    public void ResetCurrent()
    {
        Reset(_selectedMatrix);
    }
    public Dictionary<string, int[,]> Puzzles { get; set; }
    public IPuzzle Puzzle { get; set; }
    public Cell Cell(int id) => Puzzle.Cell(id);
    public string ConsoleVersion { get; set; }
    private string _selectedMatrix;
    public string SelectedMatrix
    {
        get
        {
            return _selectedMatrix;
        }
        set
        {
            _selectedMatrix = value;
            Reset(_selectedMatrix);
        }
    }
    public string LocalConsole { get; set; }
    public bool AlertIsActive { get; set; }
    public bool SolveHasStarted => Puzzle.SolveHasStarted;
    public bool? IsSolved { get; set; }
    public decimal? StopwatchTime => Puzzle.StopwatchTime;

    public void SolveConsole()
    {
        throw new NotImplementedException();
    }
    public void SolveGui()
    {
        IsSolved = Puzzle.Solve();
    }
    public void CloseAlert()
    {
        AlertIsActive = false;
    }
}
