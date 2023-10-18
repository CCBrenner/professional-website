namespace ProfessionalWebsite.Client.Services.SudokuService;

public interface IPuzzle
{
    bool SolveHasStarted { get; }
    decimal StopwatchTime { get; }
    TxnLedger Ledger { get; }
    int ProgressPercentage { get; }
    Cell Cell(int id);
    bool Solve();
    void LoadMatrixAsCellValues(int[,] matrixToLoad);
}