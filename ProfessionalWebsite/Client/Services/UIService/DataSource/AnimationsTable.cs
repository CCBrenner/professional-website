namespace ProfessionalWebsite.Client.Services.UI;

public sealed class AnimationsTable
{
    private List<bool> isContinuous = new()
    {
        false,  // Bombastic
        false,  // Skywalker
        false,  // Kitchen Sink
        false,  // Flipster
        false,  // Asteroid
        false,  // Flip On X
        false,  // Flip On Y
        false,  // Rotate on Z
        false,  // East Is Up
        false,  // West Is Up
        false,  // SloRo
    };
    public static List<bool> GetIsContinuous()
    {
        AnimationsTable animationsTable = new();
        return animationsTable.isContinuous;
    }
}
