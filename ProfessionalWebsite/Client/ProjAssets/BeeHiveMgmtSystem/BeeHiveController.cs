using ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel;

namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public sealed class BeeHiveController
{
    private BeeHiveController()
    {
        BeeHive = new BeeHive(new Settings());
    }
    private static BeeHiveController instance = null;
    private static readonly object lockobject = new();
    public static BeeHiveController Instance
    {
        get
        {
            lock (lockobject)
            {
                if (instance == null) instance = new BeeHiveController();
                return instance;
            }
        }
    }
    public BeeHive BeeHive;
    public float QueensHunger => BeeHive.Queen.CostPerShift;
    public float ConsumptionRate => BeeHive.ConsumptionRate;
    public bool HiveIsBankrupt => BeeHive.Queen.HiveIsBankrupt;
    public int CurrentDay => BeeHive.Queen.CurrentDay; 
    public bool TimerIsBeingUsed { get { return BeeHive.Queen.TimerIsBeingUsed; } set { BeeHive.Queen.TimerIsBeingUsed = value; } }
    public bool TimerRunning => BeeHive.Queen.TimerRunning;
    public string StatusReport => BeeHive.Queen.StatusReport.Replace("\n", "<br>");
    public int WorkersTotal => BeeHive.Queen.AssignedWorkersCount;

    // Honey:
    public float VaultHoney => (float)Math.Round(BeeHive.Vault.Honey, 2);
    public int HoneyManufacturers => BeeHive.Queen.GetWorkersCountByWorkerType(EWorkerType.HoneyMaker);
    public float HoneyTrajectory => (float)Math.Round((HoneyAdditionRate - ConsumptionRate), 2);
    public float HoneyAdditionRate =>
        (float)Math.Round(BeeHive.HoneyMakerBee.NectarProcessedPerShift 
            * HoneyManufacturers 
            * BeeHive.Vault.NectarConversionRatio, 2);

    // Nectar:
    public float VaultNectar => (float)Math.Round(BeeHive.Vault.Nectar, 2);
    public int NectarCollectors => BeeHive.Queen.GetWorkersCountByWorkerType(EWorkerType.NectarCollector);
    public float NectarCollectionRate =>
        (float)Math.Round(BeeHive.NectarCollectorBee.NectarCollectedPerShift * NectarCollectors, 2);
    public float NectarReductionRate =>
        (float)Math.Round(BeeHive.HoneyMakerBee.NectarProcessedPerShift * HoneyManufacturers, 2);
    public float NectarTrajectory => (float)Math.Round((NectarCollectionRate - NectarReductionRate), 2);
    public string VaultNotification => BeeHive.GetVaultNotification();

    // Eggs:
    public int Eggs => (int)QueenBee.Eggs;
    public int EggNurses => BeeHive.Queen.GetWorkersCountByWorkerType(EWorkerType.EggNurse);
    public float EggChangeRate => (float)Math.Round(EggsPerShift - EggToUnassignedConversionRate, 2);
    public float EggsPerShift => BeeHive.Queen.EggsProducedPerShift;
    public float CareProgressPerShift => BeeHive.EggCareBee.CareProgressPerShift;
    public float EggToUnassignedConversionRate => Eggs < 1 ? 0f : (float)Math.Round(CareProgressPerShift * EggNurses, 2);

    public int UnassignedWorkersCount => BeeHive.Queen.UnassignedBeeCount;
    public void AssignBee(EWorkerType workerType) => BeeHive.Queen.AssignBee(workerType);
    public void WorkTheNextShift() => BeeHive.Queen.WorkTheNextShift();
    public void Reset() => BeeHive.Reset();
    public void StartTimer() => BeeHive.Queen.StartTimer();
}
