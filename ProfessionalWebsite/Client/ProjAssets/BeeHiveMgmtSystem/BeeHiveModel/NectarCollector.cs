namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public class NectarCollector : Bee
{
    public NectarCollector(float costPerShift, float nectarProcessedPerShift) : base(EWorkerType.NectarCollector)
    {
        CostPerShift = costPerShift;
        NectarCollectedPerShift = nectarProcessedPerShift;
    }

    private Vault vault = Vault.Instance;

    public float NectarCollectedPerShift { get; private set; }
    public override float CostPerShift { get; set; }

    public override float GetCostOfThisShift() => CostPerShift;
    protected override void DoJob() =>
        vault.CollectNectar(NectarCollectedPerShift);
}
