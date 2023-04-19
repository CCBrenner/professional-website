namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class BeeHiveController
    {
        public BeeHiveController()
        {
            beeHive = new BeeHive();
        }
        public BeeHive beeHive { get; private set; }
        public int CurrentDay => 
            beeHive.Queen.CurrentDay;
        public string StatusReport =>
            beeHive.Queen.StatusReport.Replace("\n", "<br>");
        public float VaultHoney => 
            (float)(Math.Floor(beeHive.Vault.Honey * 10) / 10);
        public float VaultNectar => 
            (float)(Math.Floor(beeHive.Vault.Nectar * 10) / 10);
        public string VaultNotification => 
            beeHive.GetVaultNotification();
        public float Eggs => 
            beeHive.Queen.Eggs - (beeHive.Queen.Eggs % 1);
        public float UnassignedWorkersCount  => 
            (float)Math.Floor(beeHive.Queen.UnassignedWorkersCount);
        public int HoneyManufacturers => 
            beeHive.Queen.GetWorkersCountByWorkerType(WorkerType.HoneyManufacturer);
        public int NectarCollectors => 
            beeHive.Queen.GetWorkersCountByWorkerType(WorkerType.NectarCollector);
        public int EggNurses =>
            beeHive.Queen.GetWorkersCountByWorkerType(WorkerType.EggCare);
        public int WorkersTotal => 
            beeHive.Queen.AssignedWorkersCount;
        public float ConsumptionRate => 
            beeHive.ConsumptionRate;
        public bool HiveIsBankrupt => 
            beeHive.Queen.HiveIsBankrupt;
        public void AssignBee(WorkerType workerType) => beeHive.Queen.AssignBee(workerType);
        public void WorkTheNextShift() => beeHive.Queen.WorkTheNextShift();
        public void Reset() => beeHive.Reset();
    }
}
