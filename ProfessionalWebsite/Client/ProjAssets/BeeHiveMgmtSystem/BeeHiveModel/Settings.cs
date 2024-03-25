namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;

/*
public sealed class Settings
{
    private Settings() { }
    private static Settings? instance = null;
    private Settings instanceWithSavedDefaultValues = new Settings();
    private static readonly object lockObject = new object();
    public static Settings Instance 
    {
        get
        {
            lock (lockObject)
            {
                if (instance == null)
                    instance = new Settings();
                return instance;
            }
        }
    }

    // pre-game initial values
    public float QueenEggs = 0F;
    public float QueenUnassignedWorkersCount = 0F;
    public float VaultHoney = 25F;
    public float VaultNectar = 100F;

    // in-game constants
    public float HoneyManufacturerCostPerShift = 1.7F;
    public float HoneyManufacturerNectarProcessedPerShift = 33.15F;

    public float EggCareCostPerShift = 1.53F;
    public float EggCareCareProgressPerShift = 0.15F;

    public float NectarCollectorCostPerShift = 1.95F;
    public float NectarCollectarNectarCollectedPerShift = 33.25F;

    public float QueenEggsPerShift = 0.45F;
    public float QueenHoneyPerUnassignedWorker = 0.5F;
    public float QueenCostPerShift = 2.15F;

    public float VaultNectarConversionRatio = 0.19F;
    public float VaultLowLevelWarning = 10F;
    /*
    public void Reset()
    {
        instance = new Settings();
    }
    /*
    // ConcurrentDictionary could be used to solve concurrent thread access issues (if that is the problem w/Settings singleton)
    ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string, int>()
    {
        ["A"] = 12,
        ["B"] = 2,
        ["C"] = 3,
    };
    public int TestThis()
    {
        if (dictionary.TryGetValue("A", out int value)) return value;
        return 10;
    }
}
*/
