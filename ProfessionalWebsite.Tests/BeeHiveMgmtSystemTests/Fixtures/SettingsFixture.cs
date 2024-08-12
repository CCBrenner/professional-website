using ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem.BeeHiveModel;

namespace ProfessionalWebsite.Tests.BeeHiveMgmtSystemTests.Fixtures;

internal class SettingsFixture : Settings
{
    public SettingsFixture()
    {
        // Vault:
        NectarConversionRatio = 0.19F;
        LowLevelWarning = 10F;
        Honey = 25F;
        Nectar = 100F;

        // Everything else:
        QueenBeeCostPerShift = 2.15F;
        UnassignedBeeStaringAmount = 0;

        NectarCollectorCostPerShift = 1.95F;
        NectarCollectedPerShift = 33.25F;

        HoneyMakerCostPerShift = 1.7F;
        NectarProcessedPerShift = 33.15F;

        EggNurseCostPerShift = 1.53F;
        EggNurseCareProgressPerShift = 0.15F;

        StartingAmountOfEggs = 1F;
        StartingDay = 1;
        EggsProducedPerShift = 0.45F;
        HoneyConsumedPerUnassignedBee = 0.5F;
        TimerIsBeingUsed = false;
        TimerRunning = false;
    }
}
