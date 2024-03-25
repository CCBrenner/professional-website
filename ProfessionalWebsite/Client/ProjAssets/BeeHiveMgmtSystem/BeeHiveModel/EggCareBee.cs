namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public class EggCareBee : Bee
{
    public EggCareBee(QueenBee queen) : base(WorkerType.EggCare)
    {
        this.queen = queen;
    }

    private QueenBee queen;
    /*
    private Settings settings = Settings.Instance;
    public float CareProgressPerShift =>
        settings.EggCareCareProgressPerShift;
    public override float CostPerShift =>
        settings.EggCareCostPerShift;
    */
    public float CareProgressPerShift = 0.15F;
    public override float CostPerShift => 1.53F;

    protected override void DoJob() =>
        queen.CareForEggs(CareProgressPerShift);
}
