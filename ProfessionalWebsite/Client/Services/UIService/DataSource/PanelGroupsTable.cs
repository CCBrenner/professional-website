namespace ProfessionalWebsite.Client.Services.UI;

internal sealed class PanelGroupsTable
{
    internal static Dictionary<int, PanelGroup> GetPanelGroupsDict()
    {
        Dictionary<int, PanelGroup> panelGroupsDict = new();

        foreach (PanelGroup panelGroup in GetPanelGroups())
            panelGroupsDict.Add(panelGroup.Id, panelGroup);

        return panelGroupsDict;
    }
    internal static List<PanelGroup> GetPanelGroups() => new()
    {
        PanelGroup.Create(0, 4),  // _nav panels
    };
}
