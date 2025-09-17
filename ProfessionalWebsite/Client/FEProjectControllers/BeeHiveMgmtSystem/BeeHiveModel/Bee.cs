namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public abstract class Bee : IWorker
{
    public Bee(EWorkerType title)
    {
        Job = title;
    }

    public EWorkerType Job { get; set; }

    public abstract float CostPerShift { get; set; }
    public abstract float GetCostOfThisShift();

    public virtual void WorkTheNextShift()
    {
        if (Vault.Instance.ConsumeHoney(CostPerShift)) DoJob();
    }
    protected abstract void DoJob();
}
