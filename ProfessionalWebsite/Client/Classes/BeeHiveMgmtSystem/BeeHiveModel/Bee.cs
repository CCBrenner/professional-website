namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem;

public abstract class Bee : IWorker
{
    public Bee(WorkerType title)
    {
        Job = title;
    }

    public WorkerType Job { get; set; }

    public abstract float CostPerShift { get; }

    public virtual void WorkTheNextShift()
    {
        HoneyVault vault = HoneyVault.Instance;
        if (vault.ConsumeHoney(CostPerShift))
            DoJob();
    }
    protected abstract void DoJob();
}
