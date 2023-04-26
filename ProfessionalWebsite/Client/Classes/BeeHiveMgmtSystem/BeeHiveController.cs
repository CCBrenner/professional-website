namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public sealed class BeeHiveController
    {
        private BeeHiveController()
        {
            BeeHive = new BeeHive();
        }
        private static BeeHiveController instance = null;
        private static readonly object lockobject = new object();
        public static BeeHiveController Instance
        {
            get
            {
                lock (lockobject)
                {
                    if (instance == null)
                        instance = new BeeHiveController();
                    return instance;
                }
            }
        }
        public BeeHive BeeHive;
        public float QueensHunger => BeeHive.Queen.CostPerShift;
        public int CurrentDay => 
            BeeHive.Queen.CurrentDay; 
        public bool TimerIsBeingUsed { get { return BeeHive.Queen.TimerIsBeingUsed; } set { BeeHive.Queen.TimerIsBeingUsed = value; } }
        public bool TimerRunning => BeeHive.Queen.TimerRunning;
        public string StatusReport =>
            BeeHive.Queen.StatusReport.Replace("\n", "<br>");
        public float VaultHoney => 
            (float)(Math.Floor(BeeHive.Vault.Honey * 10) / 10);
        public float VaultNectar => 
            (float)(Math.Floor(BeeHive.Vault.Nectar * 10) / 10);
        public string VaultNotification => 
            BeeHive.GetVaultNotification();
        public float Eggs =>
            (float)(Math.Floor(BeeHive.Queen.Eggs * 10) / 10);
        public float UnassignedWorkersCount  => 
            (float)Math.Floor(BeeHive.Queen.UnassignedWorkersCount);
        public int HoneyManufacturers => 
            BeeHive.Queen.GetWorkersCountByWorkerType(WorkerType.HoneyManufacturer);
        public int NectarCollectors => 
            BeeHive.Queen.GetWorkersCountByWorkerType(WorkerType.NectarCollector);
        public int EggNurses =>
            BeeHive.Queen.GetWorkersCountByWorkerType(WorkerType.EggCare);
        public int WorkersTotal => 
            BeeHive.Queen.AssignedWorkersCount;
        public float ConsumptionRate => 
            BeeHive.ConsumptionRate;
        public bool HiveIsBankrupt => 
            BeeHive.Queen.HiveIsBankrupt;
        public float EggsPerShift => 
            BeeHive.Queen.EggsPerShift;
        //public float HoneyPerUnassignedWorker => BeeHive.Queen.HoneyPerUnassignedWorker;
        //public float NectarCollectedPerShift => BeeHive.NectarCollectorBee.NectarCollectedPerShift;
        public float NectarCollectionRate =>
            (float)(Math.Floor(BeeHive.NectarCollectorBee.NectarCollectedPerShift * NectarCollectors * 10) / 10);
        //public float NectarProcessedPerShift => BeeHive.HoneyManufacturerBee.NectarProcessedPerShift;
        public float NectarReductionRate =>
            (float)(Math.Floor(BeeHive.HoneyManufacturerBee.NectarProcessedPerShift * HoneyManufacturers * 10) / 10);
        public float HoneyAdditionRate =>
            (float)(Math.Floor(BeeHive.HoneyManufacturerBee.NectarProcessedPerShift * HoneyManufacturers * BeeHive.Vault.NectarConversionRatio * 10) / 10);
        public float NectarTrajectory =>
            (float)(Math.Floor((NectarCollectionRate - NectarReductionRate) * 10) / 10);
        public float HoneyTrajectory =>
            (float)(Math.Floor((HoneyAdditionRate - ConsumptionRate) * 10) / 10);
        public float CareProgressPerShift =>
            BeeHive.EggCareBee.CareProgressPerShift;
        public float EggToUnassignedConversionRate =>
            (float)(Math.Floor((CareProgressPerShift * EggNurses) * 100) / 100);
        public void AssignBee(WorkerType workerType) => BeeHive.Queen.AssignBee(workerType);
        public void WorkTheNextShift() => BeeHive.Queen.WorkTheNextShift();
        public void Reset() => BeeHive.Reset();
        public void StartTimer() => BeeHive.Queen.StartTimer();
    }
}
