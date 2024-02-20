namespace ProfessionalWebsite.Client.Classes.SudokuSolverProject;

public interface ISolver
{
    System.Timers.Timer Timer { get; }
    decimal StopwatchTime { get; }
    bool SolveHasStarted { get; }
    int ProgressPercentage { get; }
    bool Solve(Puzzle puzzle);
}
