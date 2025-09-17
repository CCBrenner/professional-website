using ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel;
using ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel.Contracts;

namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

public sealed class Vault
{
    private Vault(IBeeHiveSettings settings)
    {
        NectarConversionRatio = settings.NectarConversionRatio;
        lowLevelWarning = settings.LowLevelWarning;
        Honey = settings.Honey;
        Nectar = settings.Nectar;
    }  
    private static Vault? instance = null;
    private static readonly object lockObject = new object();
    public static Vault Instance
    {
        get
        {
            lock (lockObject)
            {
                if (instance == null) instance = new Vault(new Settings());
                return instance;
            }
        }
    }
    public void Reset()
    {
        Honey = 25F;
        Nectar = 100F;
    }

    private float lowLevelWarning;

    public float Honey { get; private set; }
    public float Nectar { get; private set; }
    public float NectarConversionRatio { get; private set; }
    public string Notifications
    { 
        get 
        {
            var result = "";
            if (Honey < lowLevelWarning)
                result += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
            if (Nectar < lowLevelWarning)
                result += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";
            return result;
        }
    }

    public string StatusReport
    {
        get
        {
            return $"\nVault report:" +
                $"\n{Honey} units of Honey" +
                $"\n{Nectar} units of Nectar" +
                $"{Notifications}";
        }
    }

    public void CollectNectar(float nectarToStore) { Nectar += nectarToStore > 0F ? nectarToStore : 0F; }
    public void ConvertNectarToHoney(float amountToConvert)
    {
        amountToConvert = amountToConvert > Nectar ? Nectar : amountToConvert;
        Nectar -= amountToConvert;
        Honey += amountToConvert * NectarConversionRatio;
    }
    public bool ConsumeHoney(float consumptionAmount)
    {
        if (consumptionAmount > Honey) return false;
        Honey -= consumptionAmount;
        return true;
    }
}
