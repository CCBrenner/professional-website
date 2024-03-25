namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

interface IWorker
{
    WorkerType Job { get; set; }
    void WorkTheNextShift();
}
