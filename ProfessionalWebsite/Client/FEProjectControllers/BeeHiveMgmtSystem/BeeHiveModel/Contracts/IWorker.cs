namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

interface IWorker
{
    EWorkerType Job { get; set; }
    void WorkTheNextShift();
    abstract float GetCostOfThisShift();
}
