﻿namespace ProfessionalWebsite.Client.Classes.PanelMgmt
{
    public class PanelMgmt
    {
        private PanelMgmt()
        {
            PanelGroupsTable panelGroupsTable = PanelGroupsTable.Instance;
            PanelsTable panelTable = PanelsTable.Instance;

            PanelGroups = panelGroupsTable.PanelGroups;
            Panels = panelTable.Panels;
        }

        private static PanelMgmt instance;
        private static object instanceLock = new object();

        public static PanelMgmt Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new PanelMgmt();
                    return instance;
                }
            }
        }

        public event Action<string> OnPanelMgmtUpdated;

        public List<PanelGroup> PanelGroups { get; private set; }
        public List<Panel> Panels { get; private set; }

        public void DeactivateAllPanels()
        {
            foreach (Panel panel in Panels)
            {
                panel.Deactivate();
                ActivateLocationButtonsOfGroups();
            }
            RaiseEventOnPanelMgmtUpdated();
        }
        public void DeactivatePanel(int selectedPanel)
        {
            Panels[selectedPanel].Deactivate();
            RaiseEventOnPanelMgmtUpdated();
        }
        public void ActivatePanel(int selectedPanel)
        {
            DeactivateAllPanels();
            Panels[selectedPanel].Activate();
            RaiseEventOnPanelMgmtUpdated();
        }
        public void TogglePanel(int selectedPanel)
        {
            if (Panels[selectedPanel].PanelStatus == "")
            {
                DeactivateAllPanels();
                Panels[selectedPanel].Activate();
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
        private void ActivateLocationButtonsOfGroups()
        {
            if (AllPanelsAreDeactivated())
            {
                for (int i = 0; i < Panels.Count; i++)
                {
                    List<Panel> panelGroup = GetPanelsOfGroup(i);
                    foreach (Panel panel in panelGroup)
                        if (panel == PanelGroups[i].LocationPanel)
                            panel.ActivateButton();
                }
            }
        }
        private bool AllPanelsAreDeactivated()
        {
            bool panelsAreDeactivated = true;
            foreach (Panel panel in Panels)
            {
                if (panel.PanelStatus != "")
                {
                    panelsAreDeactivated = false;
                    break;
                }
            }
            return panelsAreDeactivated;
        }

        /* DataTable Queries */
        private List<Panel> GetPanelsOfGroup(int groupId)
        {
            List<Panel> panelsOfGroup = new List<Panel>();

            foreach (Panel panel in Panels)
                if (panel.PanelGroupId == groupId)
                    panelsOfGroup.Add(panel);

            return panelsOfGroup;
        }
    }
}
