namespace ProfessionalWebsite.Client.Services.UI;

public static class PanelMgmt
{
    /*
    Definitions:
        - "cooperative" vs. "independent" panels: "cooperative" panels are panels that can only ever be "on" if all other cooperative panels are turned "off". "Independent" panels can stay on while a cooperative panel is on as well as when all cooperative panels are turned off. Overrides do exist for behavior of each, but defaults reflect what is described above.
    */
    public static void DeactivateAllPanels(IEnumerable<Panel> panels)
    {
        foreach (Panel panel in panels)
        {
            panel.Deactivate();
        }
    }
    public static void DeactivateCooperativePanels(List<Panel> panels)
    {
        foreach (Panel panel in panels)
        {
            if (panel.IsCooperativePanel)
            {
                panel.Deactivate();
            }
        }
    }
    public static void DeactivatePanel(int selectedPanelId, Dictionary<int, Panel> panels)
    {
        panels[selectedPanelId].Deactivate();
    }
    public static void ActivatePanel(int selectedPanelId, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        var panelsList = panels.Values.ToList();
        DeactivateCooperativePanels(panelsList);
        ActivateLocationButtonsOfPanelGroups(selectedPanelId, panelsList, panelGroupsList);
        panels[selectedPanelId].Activate();
    }
    public static void TogglePanel(int selectedPanelId, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        if (panels[selectedPanelId].PanelStatus == string.Empty)
        {
            DeactivateCooperativePanels(panels.Values.ToList());
            ActivateLocationButtonsOfPanelGroups(selectedPanelId, panels.Values.ToList(), panelGroups.Values.ToList());
            panels[selectedPanelId].Activate();
        }
        else
        {
            DeactivateCooperativePanels(panels.Values.ToList());
            ActivateLocationButtonsOfGroups(panels, panelGroups);
        }
    }
    public static void UpdateGroupLocationPanel(int panelId, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        int pgId = panels[panelId].PanelGroupId;
        if (pgId < 0) return;  // will be -1 if independent panel (has no specificed group)
        int lpId = panelGroups[pgId].LocationPanelId;
        panels[lpId].Deactivate();
        panelGroups[pgId].LocationPanelId = panelId;
        panels[panelId].ActivateButton();
    }
    public static void ActivateLocationButtonsOfGroups(Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        foreach (PanelGroup panelGroup in panelGroups.Values)
        {
            int panelId = panelGroup.LocationPanelId;
            Panel panel = panels[panelId];
            panel.ActivateButton();
        }
    }

    public static void SetBiDirectionalReferencesForPanelGroupsAndPanels(Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        foreach (var panel in panels.Values)
        {
            if (panel.PanelGroupId != -1)
            {
                var panelGroupId = panel.PanelGroupId;
                var panelGroup = panelGroups[panelGroupId];
                panel.SetPanelGroupReference(panelGroup);
                panel.PanelGroup.AddPanelReference(panel);
            }
        }
    }
    private static void ActivateLocationButtonsOfPanelGroups(int idOfPanelBeingActivated, List<Panel> panels, List<PanelGroup> panelGroups)
    {
        // use this to determine get the group of the deactivated panel:
        int panelGroupIdOfPanelBeingActivated = -1;

        // get the group of the activated panel:
        foreach (Panel panel in panels)
            if (idOfPanelBeingActivated == panel.Id)
                panelGroupIdOfPanelBeingActivated = panel.PanelGroupId;

        // highlight the button of each panelGroup's focused panel...
        foreach (PanelGroup panelGroup in panelGroups)
        {
            // ...but only if not the panelGroup of the panel being activated:
            if (panelGroupIdOfPanelBeingActivated != panelGroup.Id)
            {
                int panelId = panelGroup.LocationPanelId;
                Panel panel = panels[panelId];
                panel.ActivateButton();
            }
        }
    }
    private static bool AllCooperativePanelsAreDeactivated(List<Panel> panels)
    {
        foreach (Panel panel in panels)
            if (panel.PanelStatus != string.Empty && panel.IsCooperativePanel)
                return false;
        return true;
    }
}
