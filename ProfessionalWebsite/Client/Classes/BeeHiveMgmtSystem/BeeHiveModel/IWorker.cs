namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem;

interface IWorker
{
    WorkerType Job { get; set; }
    void WorkTheNextShift();
}
