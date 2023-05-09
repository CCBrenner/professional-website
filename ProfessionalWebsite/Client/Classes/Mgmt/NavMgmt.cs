namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class NavMgmt
    {
        private NavMgmt()
        {
            SectionedPages = SectionedPagesTable.Instance.SectionedPages;
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

        public List<SectionedPage> SectionedPages;
        public event Action<string> OnNavMgmtUpdated;

        public void NavigateToPage(int panelIndex, bool triggersOnPanelMgmtUpdated = true)
        {
            panelMgmt.DeactivateAllPanels(true, triggersOnPanelMgmtUpdated, true);
            panelIndex += 2;  // "+ 2" is the panel index offset; page [0]'s panel starts at [2]
            panelMgmt.UpdateGroupLocationPanel(panelIndex);
        }
        public void NavigateToSectionedPage(int pageIndex) =>
            NavigateToSection(pageIndex, 0);
        public void NavigateToSection(int pageId, int sectionId, bool triggersOnPanelMgmtUpdated = true)
        {
            panelMgmt.DeactivateAllPanels(true, triggersOnPanelMgmtUpdated, true);
            SectionedPage? sectionedPage = GetSectionedPage(pageId, SectionedPages);
            if (sectionedPage != null)
            {
                sectionedPage.CollapseAllShowOne(sectionId);
                panelMgmt.UpdateGroupLocationPanel(sectionedPage.LocationPanelGroupId);
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
        private SectionedPage? GetSectionedPage(int pageId, List<SectionedPage> sectionedPages)
        {
            try
            {
                return (from sectionedPage in sectionedPages
                        where sectionedPage.Id == pageId
                        select sectionedPage).FirstOrDefault();
            }
            catch (NullReferenceException nrEx)
            {
                Console.WriteLine($"Error with GET SectionedPage (by ID)\n{nrEx.Message}\n{nrEx.StackTrace}");
            }
            return default;
        }
    }
}
