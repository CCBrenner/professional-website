namespace ProfessionalWebsite.Client.Services.SudokuService;

public interface IPuzzle
{
    //TxnLedger Ledger { get; }
    List<Cell> Cells { get; }
    ISolver Solver { get; }
    Cell Cell(int id);
    bool Solve();
    void LoadMatrixAsCellValues(int[,] matrixToLoad);
    void RemoveCandidates();
    void UpdateCandidates();
}