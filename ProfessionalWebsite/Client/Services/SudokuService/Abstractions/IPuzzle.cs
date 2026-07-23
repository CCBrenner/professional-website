namespace ProfessionalWebsite.Client.Services.SudokuService;

public interface IPuzzle
{
    //TxnLedger Ledger { get; }
    List<Cell> Cells { get; }
    ISolver Solver { get; }
    Cell Cell(int id);
    double Solve();
    void LoadMatrixAsCellValues(int[,] matrixToLoad);
    void RemoveCandidates();
    void UpdateCandidates();
    void SetCellValuesOfZeroToNull();
    void SetCellValuesOfNullToZero();
}