﻿namespace ProfessionalWebsite.Client.Services.UI;

public class PanelMgmt : IPanelMgmt
{
    /*
    Definitions:
        - "cooperative" vs. "independent" panels: "cooperative" panels are panels that can only ever be "on" if all other cooperative panels are turned "off". "Independent" panels can stay on while a cooperative panel is on as well as when all cooperative panels are turned off. Overrides do exist for behavior of each, but defaults reflect what is described above.
    */
    public static PanelMgmt Create() => new();
    public void DeactivateAllPanels(IEnumerable<Panel> panels)
    {
        foreach (Panel panel in panels)
        {
            panel.Deactivate();
        }
    }
    public void DeactivateCooperativePanels(List<Panel> panels)
    {
        foreach (Panel panel in panels)
        {
            if (panel.IsCooperativePanel)
            {
                panel.Deactivate();
            }
        }
    }
    public void HighlightButtonOfFocusedCooperativePanelForEachPanelGroup(Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        foreach (Panel panel in panels.Values)
        {
            if (panel.IsCooperativePanel)
            {
                ActivateLocationButtonsOfGroups(panels, panelGroups);
            }
        }
    }
    public void DeactivatePanel(int selectedPanelId, Dictionary<int, Panel> panels)
    {
        panels[selectedPanelId].Deactivate();
    }
    public void ActivatePanel(int selectedPanelId, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        var panelsList = panels.Values.ToList();
        DeactivateCooperativePanels(panelsList);
        ActivateLocationButtonsOfPanelGroups(selectedPanelId, panelsList, panelGroupsList);
        panels[selectedPanelId].Activate();
    }
    public void TogglePanel(int selectedPanelId, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        try
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
                if (AllCooperativePanelsAreDeactivated(panels.Values.ToList()))
                {
                    HighlightButtonOfFocusedCooperativePanelForEachPanelGroup(panels, panelGroups);
                }

                DeactivatePanel(selectedPanelId, panels);
            }
        }
        catch (KeyNotFoundException knfEx)
        {
            Console.WriteLine(knfEx.Message + knfEx.StackTrace);
        }
    }
    public void UpdateGroupLocationPanel(int panelId, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        try
        {
            int pgId = panels[panelId].PanelGroupId;
            int lpId = panelGroups[pgId].LocationPanelId;
            panels[lpId].Deactivate();
            panelGroups[pgId].LocationPanelId = panelId;
            panels[panelId].ActivateButton();
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
    private void ActivateLocationButtonsOfPanelGroups(int idOfPanelBeingActivated, List<Panel> panels, List<PanelGroup> panelGroups)
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
    public void ActivateLocationButtonsOfGroups(Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        foreach (PanelGroup panelGroup in panelGroups.Values)
        {
            int panelId = panelGroup.LocationPanelId;
            Panel panel = panels[panelId];
            panel.ActivateButton();
        }
    }
    private bool AllCooperativePanelsAreDeactivated(List<Panel> panels)
    {
        foreach (Panel panel in panels)
            if (panel.PanelStatus != string.Empty && panel.IsCooperativePanel)
                return false;
        return true;
    }
}
