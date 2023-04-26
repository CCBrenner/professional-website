namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    public class PanelMgmt
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

        public List<Panel> Panels { get; set; } = new List<Panel>()
        {
            new Panel(),  // [0] Global Animations for Main
            new Panel(),  // [1] BeeHive settings
        };

        public void DeactivateAllPanels()
        {
            foreach (Panel panel in Panels)
                panel.SetPanelIsActive(false);
        }
        public void ClosePanel(int selectedPanel) =>
            Panels[selectedPanel].SetPanelIsActive(false);
        public void ActivatePanel(int selectedPanel)
        {
            DeactivateAllPanels();
            Panels[selectedPanel].SetPanelIsActive(true);
        }
    }
}
