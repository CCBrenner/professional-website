namespace ProfessionalWebsite.Client.Services.UI;

internal sealed class PanelGroupsTable
{
    internal static Dictionary<int, PanelGroup> GetDictionary()
    {
        Dictionary<int, PanelGroup> panelGroupsDict = new();

        foreach (PanelGroup panelGroup in GetList())
            panelGroupsDict.Add(panelGroup.Id, panelGroup);

        return panelGroupsDict;
    }
    internal static List<PanelGroup> GetList() => new()
    {
        PanelGroup.Create(0, 10),  // _nav panels
    };
}
