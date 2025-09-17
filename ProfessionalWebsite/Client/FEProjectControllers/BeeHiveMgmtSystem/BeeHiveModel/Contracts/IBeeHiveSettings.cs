namespace ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel.Contracts;

public interface IBeeHiveSettings
{
    // Vault:
    float NectarConversionRatio { get; set; }
    float LowLevelWarning { get; set; }
    float Honey { get; set; }
    float Nectar { get; set; }

    float QueenBeeCostPerShift { get; set; }
    int UnassignedBeeStaringAmount { get; set; }

    float NectarCollectorCostPerShift { get; set; }
    float NectarCollectedPerShift { get; set; }

    float HoneyMakerCostPerShift { get; set; }
    float NectarProcessedPerShift { get; set; }

    float EggNurseCostPerShift { get; set; }
    float EggNurseCareProgressPerShift { get; set; }
    float InitialEggConversionProgress { get; set; }

    float StartingAmountOfEggs { get; set; }
    int StartingDay { get; set; }
    float EggsProducedPerShift { get; set; }
    float HoneyConsumedPerUnassignedBee { get; set; }
    bool TimerIsBeingUsed { get; set; }
    bool TimerRunning { get; set; }
}
