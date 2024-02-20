namespace ProfessionalWebsite.Client.Classes.SudokuSolverProject;

public interface ISudoku
{
    IPuzzle Puzzle { get; set; }
    Dictionary<string, int[,]> Puzzles { get; set; }
    //Dictionary<int, Cell> Cell { get; }
    bool? IsSolved { get; }
    string SelectedMatrix { get; set; }
    Cell Cell(int id);
    void SolveGui();
    void ResetCurrent();
}
