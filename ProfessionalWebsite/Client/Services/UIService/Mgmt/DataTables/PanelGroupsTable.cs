namespace ProfessionalWebsite.Client.Services.UIService.Mgmt.DataTables
{
    public class PanelGroupsTable
    {
        public PanelGroupsTable()
        {
            PanelGroups = new List<PanelGroup>()
            {
                new PanelGroup(0, 4),  // NavMgmt panels
            };

            PanelGroupsDict = new Dictionary<int, PanelGroup>();
            foreach (PanelGroup panelGroup in PanelGroups)
                PanelGroupsDict.Add(panelGroup.Id, panelGroup);
        }

        public List<PanelGroup> PanelGroups { get; private set; }
        public Dictionary<int, PanelGroup> PanelGroupsDict { get; private set; }
    }
}
