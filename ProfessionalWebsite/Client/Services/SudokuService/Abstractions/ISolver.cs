namespace ProfessionalWebsite.Client.Services.SudokuService;

public interface ISolver
{
    //System.Timers.Timer Timer { get; }
    //decimal StopwatchTime { get; }
    //bool SolveHasStarted { get; }
    //int ProgressPercentage { get; }
    bool Solve();
    //void GoBackToLastCellWithUntriedCandidatesIterative();
    //void GoToNextCell();
}
