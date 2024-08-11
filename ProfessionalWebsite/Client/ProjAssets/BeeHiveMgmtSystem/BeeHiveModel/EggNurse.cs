namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public class EggNurse : Bee
{
    public EggNurse(QueenBee queen, float costPerShift, float unassignedBeeCostPerShift, float careProgressPerShift)
        : base(EWorkerType.EggNurse)
    {
        this.queen = queen;
        CostPerShift = costPerShift;
        CareProgressPerShift = careProgressPerShift;
        UnassignedBeeCostPerShift = unassignedBeeCostPerShift;
    }

    private QueenBee queen;
    
    public float CareProgressPerShift { get; private set; }
    public override float CostPerShift { get; set; }
    public float UnassignedBeeCostPerShift { get; private set; }

    public override float GetCostOfThisShift() => QueenBee.Eggs >= 1 ? CostPerShift : UnassignedBeeCostPerShift;
    public override void WorkTheNextShift()
    {
        if (QueenBee.Eggs >= 1)
            if (Vault.Instance.ConsumeHoney(CostPerShift))
                DoJob();
        else
            Vault.Instance.ConsumeHoney(UnassignedBeeCostPerShift);
    }
    protected override void DoJob() => queen.CareForEggs(CareProgressPerShift);
}
