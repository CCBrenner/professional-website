using System.Runtime.InteropServices;

namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class BeeHiveController
    {
        public BeeHiveController()
        {
            beeHive = new BeeHive();
        }
        private BeeHive beeHive;
        public float QueensHunger => beeHive.Queen.CostPerShift;
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
            (float)(Math.Floor(beeHive.Queen.Eggs * 10) / 10);
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
        public float EggsPerShift => 
            beeHive.Queen.EggsPerShift;
        //public float HoneyPerUnassignedWorker => beeHive.Queen.HoneyPerUnassignedWorker;
        //public float NectarCollectedPerShift => beeHive.NectarCollectorBee.NectarCollectedPerShift;
        public float NectarCollectionRate =>
            (float)(Math.Floor(beeHive.NectarCollectorBee.NectarCollectedPerShift * NectarCollectors * 10) / 10);
        //public float NectarProcessedPerShift => beeHive.HoneyManufacturerBee.NectarProcessedPerShift;
        public float NectarReductionRate =>
            (float)(Math.Floor(beeHive.HoneyManufacturerBee.NectarProcessedPerShift * HoneyManufacturers * 10) / 10);
        public float HoneyAdditionRate =>
            (float)(Math.Floor(beeHive.HoneyManufacturerBee.NectarProcessedPerShift * HoneyManufacturers * beeHive.Vault.NectarConversionRatio * 10) / 10);
        public float NectarTrajectory =>
            (float)(Math.Floor((NectarCollectionRate - NectarReductionRate) * 10) / 10);
        public float HoneyTrajectory =>
            (float)(Math.Floor((HoneyAdditionRate - ConsumptionRate) * 10) / 10);
        public float CareProgressPerShift =>
            beeHive.EggCareBee.CareProgressPerShift;
        public float EggToUnassignedConversionRate =>
            (float)(Math.Floor((CareProgressPerShift * EggNurses) * 100) / 100);
        public void AssignBee(WorkerType workerType) => beeHive.Queen.AssignBee(workerType);
        public void WorkTheNextShift() => beeHive.Queen.WorkTheNextShift();
        public void Reset() => beeHive.Reset();
    }
}
