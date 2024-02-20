namespace ProfessionalWebsite.Client.Services.UI;

public class PanelGroup
{
    private PanelGroup(int id, int startingLocation)
    {
        Id = id;
        LocationPanelId = startingLocation;
    }

    public readonly int Id;
    public int LocationPanelId;
    public Dictionary<int, Panel> Panels = new Dictionary<int, Panel>();
    public static PanelGroup Create(int id, int startingLocation) => new(id, startingLocation);
}
