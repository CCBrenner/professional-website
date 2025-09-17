namespace ProfessionalWebsite.Client.Services.UI;

public class PanelGroup
{
    public PanelGroup(int id, int startingLocation)
    {
        Id = id;
        LocationPanelId = startingLocation;
    }

    public readonly int Id;
    public int LocationPanelId;
    public Dictionary<int, Panel> Panels = new();
    public static PanelGroup Create(int id, int startingLocationPanel) => 
        new(id, startingLocationPanel);

    public void AddPanelReference(Panel panel)
    {
        Panels.Add(panel.Id, panel);
    }
}
