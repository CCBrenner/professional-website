namespace ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem
{
    public class BeeHive
    {
        //public Settings Settings = Settings.Api;
        public HoneyVault Vault = HoneyVault.Instance;
        public QueenBee Queen = new QueenBee();

        // For using values in view only
        public NectarCollectorBee NectarCollectorBee = new NectarCollectorBee();
        public HoneyManufacturerBee HoneyManufacturerBee = new HoneyManufacturerBee();
        public EggCareBee EggCareBee = new EggCareBee(new QueenBee());

        public float ConsumptionRate => 
            (float)(Math.Floor(Queen.TotalCostPerShift * 10) / 10);

        public void Reset()
        {
            // Settings.Set();
            Vault.Reset();
            Queen.Reset();
        }

        public string GetVaultNotification()
        {
            if (Queen.HiveIsBankrupt)
                return "YOUR HIVE WENT BANKRUPT";
            else
            {
                return Vault.Notifications
                    .Replace("LOW HONEY - ADD A HONEY MANUFACTURER\nLOW NECTAR - " +
                    "ADD A NECTAR COLLECTOR", "HONEY & NECTAR ARE LOW")
                    .Replace("LOW NECTAR - ADD A NECTAR COLLECTOR", "NECTAR IS LOW")
                    .Replace("LOW HONEY - ADD A HONEY MANUFACTURER", "HONEY IS LOW");
            }
        }
    }
}
