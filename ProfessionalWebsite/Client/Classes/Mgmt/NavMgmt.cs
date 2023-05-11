namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class NavMgmt
    {
        private NavMgmt()
        {
            sectionMgmt = SectionMgmt.Instance;
            panelMgmt = PanelMgmt.Instance;
        }

        private static NavMgmt? instance;
        private static object instanceLock = new object();
        public static NavMgmt Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                        instance = new NavMgmt();
                    return instance;
                }
            }
        }

        private PanelMgmt panelMgmt;
        private SectionMgmt sectionMgmt;
        public event Action<string> OnNavMgmtUpdated;

        public void NavigateToPage(int panelId, bool triggersOnPanelMgmtUpdated = true)
        {
            panelMgmt.DeactivateAllPanels(true, triggersOnPanelMgmtUpdated, true);
            panelMgmt.UpdateGroupLocationPanel(panelId);
        }
        public void NavigateToSection(int sectionId, bool triggersOnPanelMgmtUpdated = true)
        {
            panelMgmt.DeactivateAllPanels(true, triggersOnPanelMgmtUpdated, true);
            try
            {
                sectionMgmt.CollapseAllShowOne(sectionId);
                int locationPanelGroupId = sectionMgmt.GetLocationPanelGroupId(sectionId);
                if (locationPanelGroupId != -1)
                    panelMgmt.UpdateGroupLocationPanel(locationPanelGroupId);
            }
            catch (NullReferenceException nrEx)
            {
                Console.WriteLine(nrEx.Message + nrEx.StackTrace);
            }
        }
        public void RaiseEventOnNavMgmtUpdated()
        {
            if (OnNavMgmtUpdated != null)
                OnNavMgmtUpdated?.Invoke("");
            RaiseEventOnNavMgmtUpdated();
        }
        public void NavigateToHardCodedPage(int panelId)
        {
            panelMgmt.UpdateGroupLocationPanel(panelId);
            panelMgmt.ActivatePanel(panelId);
        }
    }
}
