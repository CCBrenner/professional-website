namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class BeeHiveController
    {
        public BeeHiveController()
        {
            beeHive = new BeeHive();
        }
        public BeeHive beeHive { get; private set; }
        public int CurrentDay { get { return beeHive.Queen.CurrentDay; } }
        public string StatusReport { get { return beeHive.Queen.StatusReport.Replace("\n", "<br>"); } }
        public float VaultHoney { get { return (float)(Math.Floor(beeHive.Vault.Honey * 10) / 10); } }
        public float VaultNectar { get { return (float)(Math.Floor(beeHive.Vault.Nectar * 10) / 10); } }
        public string VaultNotification 
        { 
            get 
            {
                if (VaultHoney >= ConsumptionRate)
                {
                    return beeHive.Vault.Notifications
                        .Replace("LOW HONEY - ADD A HONEY MANUFACTURER\nLOW NECTAR - " +
                        "ADD A NECTAR COLLECTOR", "HONEY & NECTAR ARE LOW")
                        .Replace("LOW NECTAR - ADD A NECTAR COLLECTOR", "NECTAR IS LOW")
                        .Replace("LOW HONEY - ADD A HONEY MANUFACTURER", "HONEY IS LOW");
                }
                else
                    return "YOUR HIVE WENT BANKRUPT";
            } 
        }
        public float Eggs { get { return beeHive.Queen.Eggs - (beeHive.Queen.Eggs % 1); } }
        public float UnassignedWorkers  { get { return (float)Math.Floor(beeHive.Queen.UnassignedWorkers); } }
        public int HoneyManufacturers { get { return beeHive.Queen.GetWorkerCount(WorkerType.HoneyManufacturer); } }
        public int NectarCollectors { get { return beeHive.Queen.GetWorkerCount(WorkerType.NectarCollector); } }
        public int EggNurses { get { return beeHive.Queen.GetWorkerCount(WorkerType.EggCare); } }
        public int WorkersTotal { get { return beeHive.Queen.GetWorkerCount(); } }
        public float ConsumptionRate { get { return (float)(Math.Floor(beeHive.Queen.GetCostPerShift() * 10) / 10); } }
        public void AssignBee(WorkerType workerType) => beeHive.Queen.AssignBee(workerType);
        public void WorkTheNextShift() => beeHive.Queen.WorkTheNextShift(beeHive.Vault);
        public void Reset() => beeHive.Reset();
    }
}
