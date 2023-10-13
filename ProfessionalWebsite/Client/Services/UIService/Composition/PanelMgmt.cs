namespace ProfessionalWebsite.Client.Services.UI;

public class PanelMgmt
{
    private PanelMgmt(Dictionary<int, PanelGroup> panelGroupsDictionary, Dictionary<int, Panel> panelsDictionary)
    {
        _panelGroups = panelGroupsDictionary;
        _panels = panelsDictionary;

        SetInstanceToGroupReferences();
    }

    private Dictionary<int, Panel> _panels;
    private Dictionary<int, PanelGroup> _panelGroups;

    /*
        Definitions:
            - "cooperative" vs. "independent" panels: "cooperative" panels are panels that can only ever be "on" if all other cooperative panels are turned "off". "Independent" panels can stay on while a cooperative panel is on as well as when all cooperative panels are turned off. Overrides do exist for behavior of each, but defaults reflect what is described above.
    */

    public static PanelMgmt Create(Dictionary<int, PanelGroup> panelGroupsDictionary, Dictionary<int, Panel> panelsDictionary) =>
        new(panelGroupsDictionary, panelsDictionary);

    /// <summary>
    /// Sets all panels to their default configurations (usually an "off" state).
    /// </summary>
    /// <param name="setActivePanelGroupToLocationPanel">Sets the button highlight of the current location when all panels in the panel group have been deactivated.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default to "true", this tells components that consume _panel properties to update (based on changes to state). Component must subscribe to the event to receive update commands.</param>
    /// <param name="includeIndependentPanels">Independent panels exist outside of the deactivation logic by default. If for whatever reason they should also be deactivated, then this can be set to "true".</param>
    public void DeactivateAllPanels(
        bool setActivePanelGroupToLocationPanel,
        bool includeIndependentPanels = false
    )
    {
        foreach (Panel panel in _panels.Values)
        {
            if (panel.CannotBeActiveWhileOtherCooperativePanelIsActive || includeIndependentPanels)
            {
                panel.Deactivate();
                if (setActivePanelGroupToLocationPanel && AllCooperativePanelsAreDeactivated())
                {
                    ActivateLocationButtonsOfGroups();
                }
            }
        }
    }

    /// <summary>
    /// Deactivates a panel based on the panel's ID.
    /// </summary>
    /// <param name="selectedPanelId">ID of panel to be deactivated.</param>
    public void DeactivatePanel(int selectedPanelId)
    {
        _panels.Values
            .FirstOrDefault(panel => panel.Id == selectedPanelId)
            ?.Deactivate();
    }
    /// <summary>
    /// Activates a panel based on the panel's ID.
    /// </summary>
    /// <param name="selectedPanelId">ID of panel to be activated.</param>
    /// <returns></returns>
    public void ActivatePanel(int selectedPanelId)
    {
        DeactivateAllPanels(false);
        ActivateLocationButtonsOfGroups(selectedPanelId);
        _panels.Values
            .FirstOrDefault(panel => panel.Id == selectedPanelId)
            ?.Activate();
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
            if (_panels[selectedPanelId].PanelStatus == "")
            {
                DeactivateAllPanels(false);
                ActivateLocationButtonsOfGroups(selectedPanelId);
                _panels[selectedPanelId].Activate();
            }
            else
            {
                DeactivateAllPanels(true);
                DeactivatePanel(selectedPanelId);
            }
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
            int pgId = _panels[panelId].PanelGroupId;
            int lpId = _panelGroups[pgId].LocationPanelId;
            _panels[lpId].Deactivate();
            _panelGroups[pgId].LocationPanelId = panelId;
            _panels[panelId].ActivateButton();
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
    /// Activates the location panel for each panel group.
    /// </summary>
    /// <param name="idOfPanelBeingActivated">ID of the panel that is being activated.</param>
    /// <returns></returns>
    private void ActivateLocationButtonsOfGroups(int idOfPanelBeingActivated)
    {
        if (AllCooperativePanelsAreDeactivated())
        {
            int groupOfActivatedPanel = -1;

            // use "idOfPanelBeingActivated" to find it's panel, then find & store the panelGroup ID of the panel
            foreach (PanelGroup panelGroup in _panelGroups.Values)
                foreach (Panel panel in panelGroup.Panels.Values)
                    if (idOfPanelBeingActivated == panel.Id)
                        groupOfActivatedPanel = panelGroup.Id;

            foreach (PanelGroup panelGroup in _panelGroups.Values)
            {
                if (groupOfActivatedPanel == -1)
                {
                    int panelId = panelGroup.LocationPanelId;
                    Panel panel = _panels[panelId];
                    panel.ActivateButton();
                }
            }
        }
    }
    /// <summary>
    /// Each panel group has a location panel that remembers the panel designated to the current location. When another panel in the panel group is openned, it will be turned off, but when all panels in the panel group are closed, then the button associated with the current location's panel is highlighted (as a visual indicator of where the user currently is in the app).
    /// </summary>
    /// <returns>List of panels whose buttons have been highlighted (since they are location panels).</returns>
    private void ActivateLocationButtonsOfGroups()
    {
        foreach (PanelGroup panelGroup in _panelGroups.Values)
        {
            int panelId = panelGroup.LocationPanelId;
            Panel panel = _panels[panelId];
            panel.ActivateButton();
        }
    }
    /// <summary>
    /// Determines if all cooperative panels are currently deactivated.
    /// </summary>
    /// <returns>"True" if all cooperative panels are currently deactivated.</returns>
    private bool AllCooperativePanelsAreDeactivated()
    {
        foreach (Panel panel in _panels.Values)
            if (panel.PanelStatus != "" && panel.CannotBeActiveWhileOtherCooperativePanelIsActive)
                return false;
        return true;
    }
    /// <summary>
    /// Upon initialization of panels and panel groups, one-to-many relationships are established through assigning the references based on the relationships within each panel and panel group. This is dependent on the PanelGroupId assigned to each panel.
    /// </summary>
    private void SetInstanceToGroupReferences()
    {
        foreach (Panel panel in _panels.Values)
            panel.SetInstanceToGroupRelationship(_panelGroups.Values.ToList());

        List<Section> sectionsOfPage = new List<Section>();
        foreach (Panel panel in _panels.Values)
        {
            if (panel.PanelGroupId != -1)
            {
                _panelGroups[panel.PanelGroupId]
                    .Panels
                    .Add(panel.Id, panel);
            }
        }
    }
}
