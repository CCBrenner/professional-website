using ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel.Contracts;

namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public class BeeHive
{
    public BeeHive(IBeeHiveSettings settings)
    {
        Settings = settings;
        Queen = new(settings);
        EggCareBee = new(
            Queen, 
            settings.EggNurseCostPerShift, 
            settings.HoneyConsumedPerUnassignedBee, 
            settings.EggNurseCareProgressPerShift);
        NectarCollectorBee = new(
            Settings.NectarCollectorCostPerShift, 
            Settings.NectarCollectedPerShift);
        HoneyMakerBee = new(
            settings.HoneyMakerCostPerShift, 
            settings.HoneyConsumedPerUnassignedBee, 
            settings.NectarProcessedPerShift);
    }

    public IBeeHiveSettings Settings { get; set; }

    public Vault Vault = Vault.Instance;
    public QueenBee Queen { get; private set; }

    // For using values in view only
    public NectarCollector NectarCollectorBee { get; private set; }
    public HoneyMaker HoneyMakerBee { get; private set; }
    public EggNurse EggCareBee { get; private set; }

    public float ConsumptionRate => (float)Math.Round(Queen.TotalCostPerShift, 2);

    public void Reset()
    {
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
