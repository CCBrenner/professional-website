namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public class PanelGroup
    {
        public PanelGroup(int id, int startingLocation)
        {
            Id = id;
            LocationPanelId = startingLocation;
            Panels = new List<int>();
        }

        public readonly int Id;
        public int LocationPanelId;
        public List<int> Panels;
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
