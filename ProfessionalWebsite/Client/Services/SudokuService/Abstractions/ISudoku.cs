namespace ProfessionalWebsite.Client.Services.SudokuService;

public interface ISudoku
{
    IPuzzle Puzzle { get; set; }
    Dictionary<string, int[,]> Puzzles { get; set; }
    double StopwatchTimeInSeconds { get; }
    string SelectedMatrix { get; set; }
    Cell Cell(int id);
    void SolveGui();
    void ResetCurrentPuzzle();
}
