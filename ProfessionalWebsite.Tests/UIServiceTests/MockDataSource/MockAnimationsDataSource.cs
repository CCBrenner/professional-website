namespace ProfessionalWebsite.Tests.UIServiceTests;

internal class MockAnimationsDataSource
{
    public static List<bool> GetIsContinuous() => new()
    {
        false,  // 0:Bombastic
        false,  // 1:Skywalker
        false,  // 2:Kitchen Sink
        false,  // 3:Flipster
        false,  // 4:Asteroid
        false,  // 5:Flip On Y
        false,  // 6:Flip On X
        false,  // 7:East Is Up
        false,  // 8:West Is Up
        false,  // 9:SloRo
        false,  // 10:Rotate on Z
    };
}

