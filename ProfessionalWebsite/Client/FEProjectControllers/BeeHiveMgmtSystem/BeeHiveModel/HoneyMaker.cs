namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public class HoneyMaker : Bee
{
    public HoneyMaker(float costPerShift, float unassignedBeeCostPerShift, float nectarProcessedPerShift)
        : base(EWorkerType.HoneyMaker)
    {
        CostPerShift = costPerShift;
        UnassignedBeeCostPerShift = unassignedBeeCostPerShift;
        NectarProcessedPerShift = nectarProcessedPerShift;
    }

    private Vault vault = Vault.Instance;

    public float NectarProcessedPerShift { get; private set; }
    public override float CostPerShift { get; set; }
    public float UnassignedBeeCostPerShift { get; private set; }

    public override float GetCostOfThisShift() => 
        Vault.Instance.Nectar >= NectarProcessedPerShift ? CostPerShift : UnassignedBeeCostPerShift;
    public override void WorkTheNextShift()
    {
        if (Vault.Instance.Nectar >= NectarProcessedPerShift)
            if (Vault.Instance.ConsumeHoney(CostPerShift))
                DoJob();
        else
            Vault.Instance.ConsumeHoney(UnassignedBeeCostPerShift);
    }
    protected override void DoJob()
    {
        vault.ConvertNectarToHoney(NectarProcessedPerShift);
    }
}
