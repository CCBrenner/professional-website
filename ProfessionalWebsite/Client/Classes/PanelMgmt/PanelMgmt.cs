namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    public sealed class PanelMgmt
    {
        private PanelMgmt() { }
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

        public List<Panel> Panels { get; set; } = new List<Panel>()
        {
            new Panel(  // [0] Global Animations for Main
                panelActiveStatusClassName: "anim-display", 
                mainContentClassName: "content-blur",
                behindPanelStatusClassName: "button-on-show-behind-panel"
            ),
            new Panel(),  // [1] BeeHive settings
        };

        public void DeactivateAllPanels()
        {
            foreach (Panel panel in Panels)
                panel.SetPanelIsActive(false);
            RaiseEventOnPanelMgmtUpdated();
        }
        public void ClosePanel(int selectedPanel)
        {
            Panels[selectedPanel].SetPanelIsActive(false);
            RaiseEventOnPanelMgmtUpdated();
        }
        public void ActivatePanel(int selectedPanel)
        {
            DeactivateAllPanels();
            Panels[selectedPanel].SetPanelIsActive(true);
            RaiseEventOnPanelMgmtUpdated();
        }
        public void TogglePanel(int selectedPanel)
        {
            if (Panels[selectedPanel].PanelStatus == "")
            {
                DeactivateAllPanels();
                Panels[selectedPanel].SetPanelIsActive(true);
            }
            else
               Panels[selectedPanel].TogglePanel();
            RaiseEventOnPanelMgmtUpdated();
        }
        private void RaiseEventOnPanelMgmtUpdated()
        {
            if (OnPanelMgmtUpdated != null)
                OnPanelMgmtUpdated?.Invoke("");
        }
    }
}
