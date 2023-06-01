namespace ProfessionalWebsite.Client.Services.UIService.Mgmt
{
    public class PanelMgmt
    {
        public PanelMgmt(Dictionary<int, PanelGroup> panelGroupsDictionary, Dictionary<int, Panel> panelsDictionary)
        {
            PanelGroups = panelGroupsDictionary;
            Panels = panelsDictionary;

            SetInstanceToGroupReferences();
        }

        public event Action<string> OnPanelMgmtUpdated;

        public Dictionary<int, Panel> Panels { get; private set; }
        public Dictionary<int, PanelGroup> PanelGroups { get; private set; }

        /*
            Definitions:
                - "cooperative" vs. "independent" panels: "cooperative" panels are panels that can only ever be "on" if all other cooperative panels are turned "off". "Independent" panels can stay on while a cooperative panel is on as well as when all cooperative panels are turned off. Overrides do exist for behavior of each, but defaults reflect what is described above.
        */

        /// <summary>
        /// Sets all panels to their default configurations (usually an "off" state).
        /// </summary>
        /// <param name="setActivePanelGroupToLocationPanel">Sets the button highlight of the current location when all panels in the panel group have been deactivated.</param>
        /// <param name="triggersOnPanelMgmtUpdated">Default to "true", this tells components that consume PanelMgmt properties to update (based on changes to state). Component must subscribe to the event to receive update commands.</param>
        /// <param name="includeIndependentPanels">Independent panels exist outside of the deactivation logic by default. If for whatever reason they should also be deactivated, then this can be set to "true".</param>
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

        /// <summary>
        /// Deactivates a panel based on the panel's ID.
        /// </summary>
        /// <param name="selectedPanelId">ID of panel to be deactivated.</param>
        public void DeactivatePanel(int selectedPanelId)
        {
            Panels.Values
                .FirstOrDefault(panel => panel.Id == selectedPanelId)
                ?.Deactivate();
            RaiseEventOnPanelMgmtUpdated();
        }

        /// <summary>
        /// Activates a panel based on the panel's ID.
        /// </summary>
        /// <param name="selectedPanelId">ID of panel to be activated.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Toggles a panel's state from "off" to "on" and vice versa by panel ID.
        /// </summary>
        /// <param name="selectedPanelId">ID of panel to be toggled on or off.</param>
        /// <returns></returns>
        public void TogglePanel(int selectedPanelId)
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
            }
            catch (KeyNotFoundException knfEx)
            {
                Console.WriteLine(knfEx.Message + knfEx.StackTrace);
            }
        }

        /// <summary>
        /// Sets the location panel of a panel group using only the ID of a given panel. If the panel is not part of a panel group, then nothing happens. 
        /// </summary>
        /// <param name="panelId">ID of panel to be made the location panel of the panel's panel group.</param>
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

        /// <summary>
        /// When navigating to a non-sectioned page (using an anchor element), deactivates all panels (including independent ones) and updates the location panel of the global navigation's panel group (leaving the location panel's button highlighted upon navgiation).
        /// </summary>
        /// <param name="panelId">ID of panel to be made location panel of global navigation panel group.</param>
        /// <param name="triggersOnPanelMgmtUpdated">Default "true", causes components that consume PanelMgmt to update. Component must subscribe to the event to receive update commands from PanelMgmt.</param>
        public void UpdatePanelsWhenNavigating(int panelId, bool triggersOnPanelMgmtUpdated = true)
        {
            DeactivateAllPanels(true, triggersOnPanelMgmtUpdated, true);
            UpdateGroupLocationPanel(panelId);
        }

        /// <summary>
        /// Updates the component that consumes it when a method in the PanelMgmt class that consumes this method invokes/signals that a change to the state of it has occurred.
        /// </summary>
        private void RaiseEventOnPanelMgmtUpdated()
        {
            if (OnPanelMgmtUpdated != null)
                OnPanelMgmtUpdated?.Invoke("");
        }

        /// <summary>
        /// Activates the location panel for each panel group.
        /// </summary>
        /// <param name="idOfPanelBeingActivated">ID of the panel that is being activated.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Each panel group has a location panel that remembers the panel designated to the current location. When another panel in the panel group is openned, it will be turned off, but when all panels in the panel group are closed, then the button associated with the current location's panel is highlighted (as a visual indicator of where the user currently is in the app).
        /// </summary>
        /// <returns>List of panels whose buttons have been highlighted (since they are location panels).</returns>
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

        /// <summary>
        /// Determines if all cooperative panels are currently deactivated.
        /// </summary>
        /// <returns>"True" if all cooperative panels are currently deactivated.</returns>
        private bool AllCooperativePanelsAreDeactivated()
        {
            foreach (Panel panel in Panels.Values)
                if (panel.PanelStatus != "" && panel.CannotBeActiveWhileOtherPanelsAreActive)
                    return false;
            return true;
        }

        /// <summary>
        /// Upon initialization of panels and panel groups, one-to-many relationships are established through assigning the references based on the relationships within each panel and panel group. This is dependent on the PanelGroupId assigned to each panel.
        /// </summary>
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
