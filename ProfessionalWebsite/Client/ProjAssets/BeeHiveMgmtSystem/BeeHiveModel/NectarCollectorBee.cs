namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public class NectarCollectorBee : Bee
{
    public NectarCollectorBee() : base(WorkerType.NectarCollector) { }

    /*
    private Settings settings = Settings.Instance;
    public float NectarCollectedPerShift =>
        settings.NectarCollectarNectarCollectedPerShift;
    public override float CostPerShift =>
        settings.NectarCollectorCostPerShift;
    */
    private HoneyVault vault = HoneyVault.Instance;

    public float NectarCollectedPerShift = 33.25F;
    public override float CostPerShift => 1.95F;

    protected override void DoJob() =>
        vault.CollectNectar(NectarCollectedPerShift);
}
