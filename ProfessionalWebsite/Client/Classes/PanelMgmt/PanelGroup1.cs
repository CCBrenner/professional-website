namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    /*
    public class PanelGroup1
    {
        public PanelGroup1(List<Panel1> panelList, bool usesLocationMemory = true, int panelIsStartingLocationIndex = 1000, bool isOn = true)
        {
            this.isOn = isOn;
            Panels = panelList;
            this.usesLocationMemory = usesLocationMemory;

            if (this.usesLocationMemory && panelIsStartingLocationIndex <= Panels.Count())
                locationPanel = Panels[panelIsStartingLocationIndex];

            if (Panels.Count() == 1)
                this.usesLocationMemory = false;
        }

        private bool usesLocationMemory;
        private Panel1? locationPanel;
        private bool isOn;

        public List<Panel1> Panels { get; private set; }

        public void DeactivateAllPanels()
        {
            if (isOn)
            {
                foreach (Panel1 panel in Panels)
                    panel.SetPanelIsActive(false);

                if (usesLocationMemory && locationPanel != null)
                    locationPanel.SetPanelButtonIsActive(true);
            }
        }
        public void DeactivatePanel(int selectedPanel)
        {
            if (isOn)
            {
                Panels[selectedPanel].SetPanelIsActive(false);

                if (usesLocationMemory && locationPanel != null)
                    locationPanel.SetPanelButtonIsActive(true);
            }
        }
        public void ActivatePanel(int selectedPanel)
        {
            if (isOn)
            {
                DeactivateAllPanels();
                Panels[selectedPanel].SetPanelIsActive(true);
            }
        }
        public void TogglePanel(int selectedPanel)
        {
            if (isOn)
            {
                if (Panels[selectedPanel].PanelStatus == "")
                    ActivatePanel(selectedPanel);
                else
                    DeactivatePanel(selectedPanel);
            }
        }
    }*/
}
