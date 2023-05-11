using static System.Collections.Specialized.BitVector32;

namespace ProfessionalWebsite.Client.Classes.Mgmt
{
    public sealed class PanelMgmt
    {
        private PanelMgmt()
        {
            PanelGroups = PanelGroupsTable.Instance.PanelGroups;
            Panels = PanelsTable.Instance.Panels;

            SetInstanceToGroupReferences();
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

        public Dictionary<int, PanelGroup> PanelGroups { get; private set; } = new Dictionary<int, PanelGroup>();
        public Dictionary<int, Panel> Panels { get; private set; } = new Dictionary<int,Panel>();

        public void DeactivateAllPanels(
            bool setActivePanelGroupToLocationPanel, 
            bool triggersOnPanelMgmtUpdated = true, 
            bool includeIndependentPanels = false
        )
        {
            foreach (Panel panel in Panels.Values)
            {
                if (panel.CannotBeActiveWhileOtherPanelsAreActive || includeIndependentPanels)
                {
                    panel.Deactivate();
                    if (setActivePanelGroupToLocationPanel)
                        ActivateLocationButtonsOfGroups();
                }
            }
            if (triggersOnPanelMgmtUpdated)
                RaiseEventOnPanelMgmtUpdated();
        }
        public void DeactivatePanel(int selectedPanelId)
        {
            Panels.Values
                .FirstOrDefault(panel => panel.Id == selectedPanelId)
                ?.Deactivate();
            RaiseEventOnPanelMgmtUpdated();
        }
        public Panel ActivatePanel(int selectedPanelId)
        {
            DeactivateAllPanels(false);
            ActivateLocationButtonsOfGroups(selectedPanelId);
            Panels.Values
                .FirstOrDefault(panel => panel.Id == selectedPanelId)
                ?.Activate();
            RaiseEventOnPanelMgmtUpdated();
            return Panels[selectedPanelId];
        }
        public Panel? TogglePanel(int selectedPanelId)
        {
            try
            {
                if (Panels[selectedPanelId].PanelStatus == "")
                {
                    DeactivateAllPanels(false);
                    ActivateLocationButtonsOfGroups(selectedPanelId);
                    Panels[selectedPanelId].Activate();
                }
                else
                {
                    DeactivateAllPanels(true);
                    DeactivatePanel(selectedPanelId);
                }
                RaiseEventOnPanelMgmtUpdated();
                return Panels[selectedPanelId];
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine(knfEx.Message + knfEx.StackTrace);
            }
            return default;
        }
        public void UpdateGroupLocationPanel(int panelId)
        {
            try
            {
                int pgId = Panels[panelId].PanelGroupId;
                int lpId = PanelGroups[pgId].LocationPanelId;
                Panels[lpId].Deactivate();
                PanelGroups[pgId].LocationPanelId = panelId;
                Panels[panelId].ActivateButton();
                RaiseEventOnPanelMgmtUpdated();
            }
            catch (ArgumentOutOfRangeException aoorEx)
            {
                Console.WriteLine($"{aoorEx.Message}\n{aoorEx.StackTrace} - origin method: PanelMgmt.UpdateGroupLocationPanel()");
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine($"{knfEx.Message}\n{knfEx.StackTrace} - origin method: PanelMgmt.UpdateGroupLocationPanel()");
            }
        }
        public void UpdatePanelsWhenNavigating(int panelId, bool triggersOnPanelMgmtUpdated = true)
        {
            DeactivateAllPanels(true, triggersOnPanelMgmtUpdated, true);
            UpdateGroupLocationPanel(panelId);
        }
        private void RaiseEventOnPanelMgmtUpdated()
        {
            if (OnPanelMgmtUpdated != null)
                OnPanelMgmtUpdated?.Invoke("");
        }
        private List<Panel> ActivateLocationButtonsOfGroups(int idOfPanelBeingActivated)
        {
            List<Panel> locationPanelsActivated = new List<Panel>();
            if (AllCooperativePanelsAreDeactivated())
            {
                int groupOfActivatedPanel = -1;

                // use "idOfPanelBeingActivated" to find it's panel, then find & store the panelGroup ID of the panel
                foreach (PanelGroup panelGroup in PanelGroups.Values)
                    foreach (Panel panel in panelGroup.Panels.Values)
                        if (idOfPanelBeingActivated == panel.Id)
                            groupOfActivatedPanel = panelGroup.Id;

                foreach (PanelGroup panelGroup in PanelGroups.Values)
                { 
                    if (groupOfActivatedPanel == -1)
                    {
                        int panelId = panelGroup.LocationPanelId;
                        Panel panel = Panels[panelId].ActivateButton();
                        locationPanelsActivated.Add(panel);
                    }
                }
            }
            return locationPanelsActivated;
        }
        private List<Panel> ActivateLocationButtonsOfGroups()
        {
            List<Panel> locationPanelsActivated = new List<Panel>();
            if (AllCooperativePanelsAreDeactivated())
            {
                foreach (PanelGroup panelGroup in PanelGroups.Values)
                {
                    int panelId = panelGroup.LocationPanelId;
                    Panel highlightedPanel = Panels[panelId].ActivateButton();
                    locationPanelsActivated.Add(highlightedPanel);
                }
            }
            return locationPanelsActivated;
        }
        private bool AllCooperativePanelsAreDeactivated()
        {
            foreach (Panel panel in Panels.Values)
                if (panel.PanelStatus != "" && panel.CannotBeActiveWhileOtherPanelsAreActive)
                    return false;
            return true;
        }
        private void SetInstanceToGroupReferences()
        {
            foreach (Panel panel in Panels.Values)
                panel.SetInstanceToGroupRelationship(PanelGroups.Values.ToList());

            List<Section> sectionsOfPage = new List<Section>();
            foreach (Panel panel in Panels.Values)
            {
                if (panel.PanelGroupId != -1)
                {
                    PanelGroups[panel.PanelGroupId]
                        .Panels
                        .Add(panel.Id, panel);
                }
            }
        }
    }
}
