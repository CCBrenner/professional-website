using ProfessionalWebsite.Client.Classes.BeeHiveMgmtSystem;

namespace ProfessionalWebsite.Tests.BeeHiveMgmtSystemTests;

[TestClass]
public class QueenBeeTests
{
    private QueenBee queenBee;

    [TestInitialize]
    public void TestInitialize()
    {
        queenBee = new QueenBee();
        queenBee.ResetSelfAndReferencedVault();
    }

    /*
    private PropertyInfo? Reflect(Type typeOfInstance, string propertyName) =>
        typeOfInstance.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
    */

    [TestMethod]
    public void TestConstructorInitializesProperties()
    {
        QueenBee queenBeeForConstructor = new QueenBee();

        Assert.AreEqual(1, queenBeeForConstructor.CurrentDay);
        Assert.AreEqual(0F, queenBeeForConstructor.Eggs);
        Assert.AreEqual(0F, queenBeeForConstructor.UnassignedWorkersCount);
        Assert.AreEqual(2.15F, queenBeeForConstructor.CostPerShift);
        Assert.AreEqual(3, queenBee.AssignedWorkersCount);
        Assert.IsTrue(queenBeeForConstructor.StatusReport.Contains("HoneyManufacturer"));
        Assert.IsTrue(queenBeeForConstructor.StatusReport.Contains("NectarCollector"));
        Assert.IsTrue(queenBeeForConstructor.StatusReport.Contains("EggCare"));
    }

    [TestMethod]
    public void TestAssignBeeAddsWorker()
    {
        Assert.AreEqual(1, queenBee.CurrentDay);
        Assert.AreEqual(0F, queenBee.Eggs);
        Assert.AreEqual(0F, queenBee.UnassignedWorkersCount);
        Assert.AreEqual(2.15F, queenBee.CostPerShift);
        Assert.IsTrue(queenBee.StatusReport.Contains("HoneyManufacturer"));
        Assert.IsTrue(queenBee.StatusReport.Contains("NectarCollector"));
        Assert.IsTrue(queenBee.StatusReport.Contains("EggCare"));

        // Arrange
        var initialAssignedWorkerCount = queenBee.AssignedWorkersCount;
        //var workersField = Reflect(typeof(QueenBee), "workers");

        // Act
        queenBee.AssignBee(WorkerType.HoneyManufacturer);

        // Assert
        Assert.AreEqual(initialAssignedWorkerCount, queenBee.AssignedWorkersCount);
    }
    [TestMethod]
    public void TestCareForEggsAddsOneUnassignedWorkerPerEggNurtured()
    {
        // Arrange
        Assert.AreEqual(0F, queenBee.UnassignedWorkersCount);
        for (int i = 0; i < 12; i++)
            queenBee.WorkTheNextShift();

        // Act
        float initialValue = queenBee.UnassignedWorkersCount;
        queenBee.CareForEggs(3);
        float result = queenBee.UnassignedWorkersCount;

        // Assert
        Assert.AreEqual(3, (result - initialValue));
    }
    [TestMethod]
    public void TestGetWorkerCountReturnsCount()
    {
        var workerCount = queenBee.GetWorkersCountByWorkerType(WorkerType.HoneyManufacturer);

        Assert.AreEqual(1, workerCount);

        // We give them 3 unassigned workers
        for (int i = 0; i < 12; i++)
            queenBee.WorkTheNextShift();
        queenBee.CareForEggs(3);
        Assert.IsTrue(3 <= queenBee.UnassignedWorkersCount);

        queenBee.AssignBee(WorkerType.HoneyManufacturer);
        queenBee.AssignBee(WorkerType.NectarCollector);
        queenBee.AssignBee(WorkerType.HoneyManufacturer);

        workerCount = queenBee.GetWorkersCountByWorkerType(WorkerType.HoneyManufacturer);

        Assert.AreEqual(3, workerCount);
    }
    /*
    [TestMethod]
    public void TestGetCostPerShiftReturnsTotalCost()
    {
        // Need to mock this QueenBee
        // Queen = 2.15F
        // Unassigned = 0.5F
        // EggCare = 1.53F
        // Honey Manufacturers = 1.7F
        // NectarCollectors = 1.95F

        // Arrange
        for (int i = 0; i < 12; i++)
            queenBee.WorkTheNextShift();
        Console.WriteLine(queenBee.Eggs);
        Assert.IsTrue(3 <= queenBee.Eggs);

        queenBee.CareForEggs(3);
        Assert.IsTrue(3 <= queenBee.UnassignedWorkersCount);

        queenBee.AssignBee(WorkerType.HoneyManufacturer);
        queenBee.AssignBee(WorkerType.NectarCollector);
        queenBee.AssignBee(WorkerType.HoneyManufacturer);

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
        for (int i = 0; i < 12; i++)
            queenBee.WorkTheNextShift();
        Console.WriteLine(queenBee.Eggs);
        Assert.IsTrue(3 <= queenBee.Eggs);

        queenBee.CareForEggs(3);
        Assert.IsTrue(3 <= queenBee.UnassignedWorkersCount);

        queenBee.AssignBee(WorkerType.HoneyManufacturer);
        for (int i = 0; i < 5; i++)
            queenBee.WorkTheNextShift();
        queenBee.CareForEggs(1);
        Assert.AreNotEqual(1, queenBee.CurrentDay);
        Assert.AreNotEqual(0F, queenBee.Eggs);
        Assert.AreNotEqual(0F, queenBee.UnassignedWorkersCount);
        Assert.AreNotEqual(3, queenBee.AssignedWorkersCount);

        // Act
        queenBee.ResetSelfAndReferencedVault();

        // Assert
        Assert.AreEqual(1, queenBee.CurrentDay);
        Assert.AreEqual(0F, queenBee.Eggs);
        Assert.AreEqual(0F, queenBee.UnassignedWorkersCount);
        Assert.IsTrue(queenBee.StatusReport.Contains("HoneyManufacturer"));
        Assert.IsTrue(queenBee.StatusReport.Contains("NectarCollector"));
        Assert.IsTrue(queenBee.StatusReport.Contains("EggCare"));
        Assert.AreEqual(3, queenBee.AssignedWorkersCount);
    }
    /*
    [TestMethod]
    public void TestAssignBee()
    {

    }*/
}
