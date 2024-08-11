using ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel.Contracts;

namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel;

public class Settings : IBeeHiveSettings
{
    // Vault:
    public float NectarConversionRatio { get; set; } = 0.19F;
    public float LowLevelWarning { get; set; } = 10F;
    public float Honey { get; set; } = 25F;
    public float Nectar { get; set; } = 100F;

    // Everything else:
    public float QueenBeeCostPerShift { get; set; } = 2.15F;
    public int UnassignedBeeStaringAmount { get; set; } = 0;

    public float NectarCollectorCostPerShift { get; set; } = 1.95F;
    public float NectarCollectedPerShift { get; set; } = 33.25F;

    public float HoneyMakerCostPerShift { get; set; } = 1.7F;
    public float NectarProcessedPerShift { get; set; } = 33.15F;

    public float EggNurseCostPerShift { get; set; } = 1.53F;
    public float EggNurseCareProgressPerShift { get; set; } = 0.15F;
    public float InitialEggConversionProgress { get; set; } = 0;

    public float StartingAmountOfEggs { get; set; } =  0F;
    public int StartingDay { get; set; } = 1;
    public float EggsProducedPerShift { get; set; } = 0.45F;
    public float HoneyConsumedPerUnassignedBee { get; set; } = 0.5F;
    public bool TimerIsBeingUsed { get; set; } = false;
    public bool TimerRunning { get; set; } = false;
}
