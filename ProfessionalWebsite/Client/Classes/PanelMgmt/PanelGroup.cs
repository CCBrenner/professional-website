namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    public class PanelGroup
    {
        public PanelGroup(int id)
        {
            Id = id;
        }

        public int Id;
        public Panel? LocationPanel;
        /*
        public void DeactivateAllPanels()
        {
            foreach (Panel panel in Panels)
                panel.SetPanelIsActive(false);

            if (locationPanel != null)
                locationPanel.SetPanelButtonIsActive(true);
        }
        public void DeactivatePanel(int selectedPanel)
        {
            Panels[selectedPanel].SetPanelIsActive(false);

            if (locationPanel != null)
                locationPanel.SetPanelButtonIsActive(true);
        }
        public void ActivatePanel(int selectedPanel)
        {
            DeactivateAllPanels();
            Panels[selectedPanel].SetPanelIsActive(true);
        }
        public void TogglePanel(int selectedPanel)
        {
            if (Panels[selectedPanel].PanelStatus == "")
                ActivatePanel(selectedPanel);
            else
                DeactivatePanel(selectedPanel);
        }*/
    }
}
