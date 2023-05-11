namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class PanelGroupsTable
    {
        private PanelGroupsTable()
        {
            List<PanelGroup> panelGroups = new List<PanelGroup>()
            {
                new PanelGroup(0, 4),  // NavMgmt panels
            };
            foreach (PanelGroup panelGroup in panelGroups)
                PanelGroups.Add(panelGroup.Id, panelGroup);

        }
        private static PanelGroupsTable? instance;
        private static object instanceLock = new object();
        public static PanelGroupsTable Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new PanelGroupsTable();
                    return instance;
                }
            }
        }

        public Dictionary<int, PanelGroup> PanelGroups = new Dictionary<int, PanelGroup>();
    }
}
