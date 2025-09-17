namespace ProfessionalWebsite.Client.Services.SudokuService;

public sealed class SudokuService : ISudoku
{
    private const string DEFAULT_PUZZLE = "Custom";
    public SudokuService()
    {
        Reset(DEFAULT_PUZZLE);
    }
    public Dictionary<string, int[,]> Puzzles { get; set; }
    public IPuzzle Puzzle { get; set; }
    public Cell Cell(int id) => Puzzle.Cell(id);
    public bool IsConsoleVersion { get; set; }
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
    public bool? IsSolved { get; set; }
    //public bool SolveHasStarted => Puzzle.SolveHasStarted;
    //public decimal? StopwatchTime => Puzzle.StopwatchTime;
    private void Reset(string puzzleBookSelection)
    {
        _selectedMatrix = puzzleBookSelection;
        IsConsoleVersion = false;
        Puzzle = Services.SudokuService.Puzzle.Create();

        if (_selectedMatrix != DEFAULT_PUZZLE)
        {
            int[,] matrixToLoad = PuzzleBook.GetPuzzle(_selectedMatrix);
            Puzzle.LoadMatrixAsCellValues(matrixToLoad);
        }

        Puzzles = PuzzleBook.GetPuzzles();
        LocalConsole = string.Empty;
        IsSolved = null;
    }
    public void ResetCurrentPuzzle()
    {
        Reset(_selectedMatrix);
    }
    public void SolveGui()
    {
        IsSolved = Puzzle.Solve();
    }
    /*
    public static SudokuService Create() => new();
    public void SolveConsole()
    {
        throw new NotImplementedException();
    }
    public void CloseAlert()
    {
        AlertIsActive = false;
    }
    */
}
