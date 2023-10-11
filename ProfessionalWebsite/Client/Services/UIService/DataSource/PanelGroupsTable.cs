namespace ProfessionalWebsite.Client.Services.UI;

public sealed class PanelGroupsTable
{
    public static Dictionary<int, PanelGroup> GetPanelGroupsDict()
    {
        Dictionary<int, PanelGroup> panelGroupsDict = new();

        foreach (PanelGroup panelGroup in GetPanelGroups())
            panelGroupsDict.Add(panelGroup.Id, panelGroup);

        return panelGroupsDict;
    }
    public static List<PanelGroup> GetPanelGroups() => new()
    {
        PanelGroup.Create(0, 4),  // _nav panels
    };
}
