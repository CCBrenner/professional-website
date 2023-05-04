namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class NavMgmt
    {
        private NavMgmt()
        {
            SectionedPages = SectionedPagesTable.Instance.SectionedPages;
            panelMgmt = PanelMgmt.Instance;
        }

        private static NavMgmt instance;
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
        public void NavigateToSection(int pageIndex, int sectionIndex, bool triggersOnPanelMgmtUpdated = true)
        {
            try
            {
                panelMgmt.DeactivateAllPanels(true, triggersOnPanelMgmtUpdated, true);
                SectionedPages[pageIndex].CollapseAllShowOne(sectionIndex);
                int panelIndex = pageIndex + 2;  // "+ 2" is the panel index offset; page [0]'s panel starts at [2]
                panelMgmt.UpdateGroupLocationPanel(panelIndex);
            }
            catch (ArgumentOutOfRangeException aoorEx)
            {
                Console.WriteLine($"{aoorEx.Message} - origin method: NavMgmt.NavigateToSection()");
            }
        }
        public void RaiseEventOnNavMgmtUpdated()
        {
            if (OnNavMgmtUpdated != null)
                OnNavMgmtUpdated?.Invoke("");
            RaiseEventOnNavMgmtUpdated();
        }
        public async void NavigateToHardCodedPage(int panelId)
        {
            panelMgmt.UpdateGroupLocationPanel(panelId);
            await panelMgmt.ActivatePanel(panelId);
        }
        /*
        public void NavigateToSection(string pagePath, int sectionIndex)
        {
            int pageIndex = 1000;  // requirement: must be greater than SectionedPages.Count()
            for (int i = 0; i < SectionedPages.Count; i++)
            {
                if (SectionedPages[i].PagePath == pagePath)
                {
                    pageIndex = i;
                    break;
                }
            }
            NavigateToSection(pageIndex, sectionIndex);
        }
        */
    }
}
