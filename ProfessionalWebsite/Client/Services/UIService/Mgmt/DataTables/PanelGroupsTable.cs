namespace ProfessionalWebsite.Client.Services.UI.Mgmt.DataTables;

public sealed class PanelGroupsTable
{
    private List<PanelGroup> panelGroups = new()
    {
        new PanelGroup(0, 4),  // Nav panels
    };

    public static List<PanelGroup> GetPanelGroups()
    {
        PanelGroupsTable panelGroupTable = new();
        return panelGroupTable.panelGroups;
    }
    public static Dictionary<int, PanelGroup> GetPanelGroupsDict()
    {
        PanelGroupsTable panelGroupTable = new();
        Dictionary<int, PanelGroup> panelGroupsDict = new Dictionary<int, PanelGroup>();

        foreach (PanelGroup panelGroup in panelGroupTable.panelGroups)
            panelGroupsDict.Add(panelGroup.Id, panelGroup);

        return panelGroupsDict;
    }
}
