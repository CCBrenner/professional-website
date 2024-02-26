namespace ProfessionalWebsite.Tests;

[TestClass]
public class ScratchTests
{
    private record MyRecord(string someString, int someInt);

    [TestMethod]
    public void TestObjectsAndRecordsEquality()
    {
        List<int> firstList = new() { 1, 2, 3 };
        List<int> secondList = new(firstList);

        Assert.AreNotEqual(secondList, firstList);
        // in C#, equality is based on refences; if they're different value objects then they are not the same object,
        // even if they have the same values within them; "reference" equality

        // records are different; they utilize structural and value equality:
        MyRecord recOne = new("sup", 6);
        MyRecord recTwo = new(recOne.someString, recOne.someInt);

        Assert.AreEqual(recOne, recTwo);
    }
}
