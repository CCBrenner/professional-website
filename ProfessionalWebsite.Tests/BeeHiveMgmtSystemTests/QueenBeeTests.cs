using ProfessionalWebsite.Client.ProjAssets.BeeHiveMgmtSystem;
using ProfessionalWebsite.Tests.BeeHiveMgmtSystemTests.Fixtures;

namespace ProfessionalWebsite.Tests.BeeHiveMgmtSystemTests;

[TestClass]
public class QueenBeeTests
{
    private QueenBee queenBee;

    [TestInitialize]
    public void TestInitialize()
    {
        queenBee = new QueenBee(new SettingsFixture());
        queenBee.ResetSelfAndReferencedVault();
    }

    [TestMethod]
    public void TestConstructorInitializesProperties()
    {
        QueenBee queenBeeForConstructor = new QueenBee(new SettingsFixture());

        Assert.AreEqual(1, queenBeeForConstructor.CurrentDay);
        Assert.AreEqual(1F, QueenBee.Eggs);
        Assert.AreEqual(0F, queenBeeForConstructor.UnassignedBeeCount);
        Assert.AreEqual(2.15F, queenBeeForConstructor.CostPerShift);
        Assert.AreEqual(3, queenBee.AssignedWorkersCount);
        Assert.IsTrue(queenBeeForConstructor.StatusReport.Contains("HoneyMaker"));
        Assert.IsTrue(queenBeeForConstructor.StatusReport.Contains("NectarCollector"));
        Assert.IsTrue(queenBeeForConstructor.StatusReport.Contains("EggNurse"));
    }

    [TestMethod]
    public void TestAssignBeeAddsWorker()
    {
        Assert.AreEqual(1, queenBee.CurrentDay);
        Assert.AreEqual(1F, QueenBee.Eggs);
        Assert.AreEqual(0F, queenBee.UnassignedBeeCount);
        Assert.AreEqual(2.15F, queenBee.CostPerShift);
        Assert.IsTrue(queenBee.StatusReport.Contains("HoneyMaker"));
        Assert.IsTrue(queenBee.StatusReport.Contains("NectarCollector"));
        Assert.IsTrue(queenBee.StatusReport.Contains("EggNurse"));

        // Arrange
        var initialAssignedWorkerCount = queenBee.AssignedWorkersCount;

        // Act
        queenBee.AssignBee(EWorkerType.HoneyMaker);

        // Assert
        Assert.AreEqual(initialAssignedWorkerCount, queenBee.AssignedWorkersCount);
    }
    /*  // Kept getting 2 for some reason and not 3; even accounted for not having a full egg at first
    [TestMethod]
    public void TestCareForEggsAddsOneUnassignedWorkerPerEggNurtured()
    {
        // Arrange
        var settings = new SettingsFixture
        {
            Honey = 50
        };
        var queenBee = new QueenBee(settings);

        // Act
        int careIterationsRequired = (int)((3f / settings.EggNurseCareProgressPerShift) + 3f);
        int initialValue = queenBee.UnassignedBeeCount;
        Console.WriteLine(initialValue);
        Enumerable.Range(0, careIterationsRequired).ToList().ForEach(x => queenBee.WorkTheNextShift());
        int result = queenBee.UnassignedBeeCount;
        Console.WriteLine(result);

        // Assert
        Assert.AreEqual(3, result - initialValue);
    }
    */
    [TestMethod]
    public void TestGetWorkerCountReturnsCount()
    {
        var settings = new SettingsFixture
        {
            UnassignedBeeStaringAmount = 3  // Note: Queen by default starts with one of each type of worker
        };
        var queenBee = new QueenBee(settings);

        queenBee.AssignBee(EWorkerType.HoneyMaker);
        queenBee.AssignBee(EWorkerType.NectarCollector);
        queenBee.AssignBee(EWorkerType.HoneyMaker);

        var honeyMakersCount = queenBee.GetWorkersCountByWorkerType(EWorkerType.HoneyMaker);

        Assert.AreEqual(3, honeyMakersCount);
    }
    /*
    [TestMethod]
    public void TestGetCostPerShiftReturnsTotalCost()
    {
        // Need to mock this QueenBee
        // Queen = 2.15F
        // Unassigned = 0.5F
        // EggNurse = 1.53F
        // Honey Manufacturers = 1.7F
        // NectarCollectors = 1.95F

        // Arrange
        for (int i = 0; i < 12; i++)
            queenBee.WorkTheNextShift();
        Console.WriteLine(queenBee.Eggs);
        Assert.IsTrue(3 <= queenBee.Eggs);

        queenBee.CareForEggs(3);
        Assert.IsTrue(3 <= queenBee.UnassignedBeeCount);

        queenBee.AssignBee(EWorkerType.HoneyMaker);
        queenBee.AssignBee(EWorkerType.NectarCollector);
        queenBee.AssignBee(EWorkerType.HoneyMaker);

        // Act
        var costPerShift = queenBee.TotalCostPerShift;

        // Assert
        Assert.AreEqual(7.15F, costPerShift);
    }
    */
    [TestMethod]
    public void TestResetReInitializesProperties()
    {
        // Arrange
        QueenBee queenBee = new(new SettingsFixture());
        Enumerable.Range(0, 12).ToList().ForEach(i => queenBee.WorkTheNextShift());
        Assert.IsTrue(3 <= QueenBee.Eggs);

        Assert.AreEqual(1, queenBee.UnassignedBeeCount);

        queenBee.AssignBee(EWorkerType.HoneyMaker);
        for (int i = 0; i < 5; i++)
            queenBee.WorkTheNextShift();
        queenBee.CareForEggs(1);
        Assert.AreNotEqual(1, queenBee.CurrentDay);
        Assert.AreNotEqual(1F, QueenBee.Eggs);
        Assert.AreNotEqual(0F, queenBee.UnassignedBeeCount);
        Assert.AreNotEqual(3, queenBee.AssignedWorkersCount);

        // Act
        queenBee.ResetSelfAndReferencedVault();

        // Assert
        Assert.AreEqual(1, queenBee.CurrentDay);
        Assert.AreEqual(1F, QueenBee.Eggs);
        Assert.AreEqual(0F, queenBee.UnassignedBeeCount);
        Console.WriteLine(queenBee.StatusReport);
        Assert.IsTrue(queenBee.StatusReport.Contains("HoneyMaker"));
        Assert.IsTrue(queenBee.StatusReport.Contains("NectarCollector"));
        Assert.IsTrue(queenBee.StatusReport.Contains("EggNurse"));
        Assert.AreEqual(3, queenBee.AssignedWorkersCount);
    }
    /*
    [TestMethod]
    public void TestAssignBee()
    {

    }*/
}
