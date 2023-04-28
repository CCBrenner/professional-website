namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    /*
    public sealed class PanelMgmt1
    {
        private PanelMgmt1()
        {
            panelGroups = new List<PanelGroup>()
            {
                new PanelGroup(new List<Panel>(){
                    new Panel(    // [0] Global Animations for Main
                    panelActiveStatusClassName: "anim-display",
                    mainContentClassName: "content-blur",
                    behindPanelStatusClassName: "button-on-show-behind-panel"
                )}),
                new PanelGroup(new List<Panel>(){
                    new Panel(),  // [1] BeeHive settings
                }),
                new PanelGroup(new List<Panel>(){
                    new Panel(    // [2] "projects" page
                        panelActiveStatusClassName: "panel-visible",
                        mainContentClassName: "content-blur",
                        behindPanelStatusClassName: "button-on-show-behind-panel",
                        panelButtonClassName: "highlight-button"
                    ),
                    new Panel(    // [3] "knowhow" page
                        panelActiveStatusClassName: "panel-visible",
                        mainContentClassName: "content-blur",
                        behindPanelStatusClassName: "button-on-show-behind-panel",
                        panelButtonClassName: "highlight-button"
                    ),
                    new Panel(    // [4] "collyn" page
                        panelActiveStatusClassName: "panel-visible",
                        mainContentClassName: "content-blur",
                        behindPanelStatusClassName: "button-on-show-behind-panel",
                        panelButtonClassName: "highlight-button"
                    ),
                    new Panel(    // [5] "invent" page
                        panelActiveStatusClassName: "panel-visible",
                        mainContentClassName: "content-blur",
                        behindPanelStatusClassName: "button-on-show-behind-panel",
                        panelButtonClassName: "highlight-button"
                    ),
                    new Panel(    // [6] "articles" page
                        panelActiveStatusClassName: "panel-visible",
                        mainContentClassName: "content-blur",
                        behindPanelStatusClassName: "button-on-show-behind-panel",
                        panelButtonClassName: "highlight-button"
                    ),
                }),
            };
        }
        private static PanelMgmt instance;
        private static object instanceLock = new object();
        public static PanelMgmt Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                        instance = new PanelMgmt();
                    return instance;
                }
            }
        }

        public event Action<string> OnPanelMgmtUpdated;

        private List<PanelGroup> panelGroups;

        public string GetPanelValue(int selectedPanel, PanelProps panelProps)
        {
            (int, int) i = TranslateMgmtIndexToGroupsIndices(selectedPanel);
            if (i.Item1 < panelGroups.Count())
                if (i.Item2 < panelGroups[i.Item1].Panels.Count())
                    return panelGroups[i.Item1].Panels[i.Item2].GetPropValue(panelProps);
            return "";
        }
        /*
        private int TranslateGroupsIndiciesToMgmtIndex(int group, int panel)
        {
            int currentIndex = 0;
            for (int i = 0; i < panelGroups.Count(); i++)
            {
                for (int j = 0; j < panelGroups.Count(); j++)
                {
                    if (i == group && j == panel)
                        return currentIndex;
                    currentIndex++;
                }
            }
            return 1000000;
        }/
        private (int, int) TranslateMgmtIndexToGroupsIndices(int panelIndex)
        {
            int currentIndex = 0;
            for (int i = 0; i < panelGroups.Count(); i++)
            {
                for (int j = 0; j < panelGroups.Count(); j++)
                {
                    if (panelIndex == currentIndex)
                        return (i, j);
                    currentIndex++;
                }
            }
            return (1000000, 1000000);
        }
        public void DeactivateAllPanels()
        {
            foreach (PanelGroup panelGroup in panelGroups)
                panelGroup.DeactivateAllPanels();
            RaiseEventOnPanelMgmtUpdated();
        }
        public void ClosePanel(int selectedPanel)
        {
            (int, int) i = TranslateMgmtIndexToGroupsIndices(selectedPanel);
            panelGroups[i.Item1].DeactivatePanel(i.Item2);
            RaiseEventOnPanelMgmtUpdated();
        }
        public void ActivatePanel(int selectedPanel)
        {
            DeactivateAllPanels();

            (int, int) i = TranslateMgmtIndexToGroupsIndices(selectedPanel);
            panelGroups[i.Item1].ActivatePanel(i.Item2);

            RaiseEventOnPanelMgmtUpdated();
        }
        public void TogglePanel(int selectedPanel)
        {
            (int, int) i = TranslateMgmtIndexToGroupsIndices(selectedPanel);

            if (panelGroups[i.Item1].Panels[i.Item2].PanelStatus == "")
            {
                DeactivateAllPanels();
                panelGroups[i.Item1].ActivatePanel(i.Item2);
            }
            else
                panelGroups[i.Item1].TogglePanel(i.Item2);

            RaiseEventOnPanelMgmtUpdated();
        }
        private void RaiseEventOnPanelMgmtUpdated()
        {
            if (OnPanelMgmtUpdated != null)
                OnPanelMgmtUpdated?.Invoke("");
        }
    }*/
}
