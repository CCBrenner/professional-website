
namespace ProfessionalWebsite.Client.Services.UI;

public class PanelService
{
    /*
    Definitions:
        - "cooperative" vs. "independent" panels: "cooperative" panels are panels that can only ever be "on" if all other cooperative panels are turned "off". "Independent" panels can stay on while a cooperative panel is on as well as when all cooperative panels are turned off. Overrides do exist for behavior of each, but defaults reflect what is described above.
    */
    private PanelService(int startingPanelId)
    {
        Panels = PanelsTable.GetDictionary();
        PanelGroups = PanelGroupsTable.GetDictionary();
        SetBiDirectionalReferencesForPanelGroupsAndPanels(PanelGroups);
        Panels[startingPanelId].ActivateButton();
    }
    public static PanelService Create(int startingPanelId)
    {
        return new PanelService(startingPanelId);
    }
    public Dictionary<int, Panel> Panels { get; private set; }
    public Dictionary<int, PanelGroup> PanelGroups { get; private set; }
    public void DeactivateAllPanels()
    {
        foreach (var pair in Panels.ToList())
        {
            pair.Value.Deactivate();
        }
    }
    public void DeactivateCooperativePanels()
    {
        foreach (var pair in Panels.ToList())
        {
            if (pair.Value.IsCooperativePanel)
            {
                pair.Value.Deactivate();
            }
        }
    }
    public void DeactivatePanel(int selectedPanelId)
    {
        Panels[selectedPanelId].Deactivate();
    }
    public void ActivatePanel(int selectedPanelId)
    {
        DeactivateCooperativePanels();
        ActivateLocationButtonsOfPanelGroups(selectedPanelId);
        Panels[selectedPanelId].Activate();
    }
    public void TogglePanel(int selectedPanelId)
    {
        if (Panels[selectedPanelId].PanelStatus == string.Empty)
        {
            DeactivateCooperativePanels();
            ActivateLocationButtonsOfPanelGroups(selectedPanelId);
            Panels[selectedPanelId].Activate();
        }
        else
        {
            Panels[selectedPanelId].Deactivate();
            DeactivateCooperativePanels();
            ActivateLocationButtonsOfGroups();
        }
    }
    public void UpdateGroupLocationPanel(int panelId)
    {
        int pgId = Panels[panelId].PanelGroupId;
        if (pgId < 0) return;  // will be -1 if independent panel (has no specificed group)
        int lpId = PanelGroups[pgId].LocationPanelId;
        Panels[lpId].Deactivate();
        PanelGroups[pgId].LocationPanelId = panelId;
        Panels[panelId].ActivateButton();
    }
    public void ActivateLocationButtonsOfGroups()
    {
        foreach (PanelGroup panelGroup in PanelGroups.Values)
        {
            int panelId = panelGroup.LocationPanelId;
            Panel panel = Panels[panelId];
            panel.ActivateButton();
        }
    }

    public void SetBiDirectionalReferencesForPanelGroupsAndPanels(Dictionary<int, PanelGroup> panelGroups)
    {
        foreach (var panel in Panels.Values)
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
    public void ActivateLocationButtonsOfPanelGroups(int idOfPanelBeingActivated)
    {
        List<PanelGroup> panelGroups = PanelGroups.Values.ToList();

        // use this to determine get the group of the deactivated panel:
        int panelGroupIdOfPanelBeingActivated = -1;

        // get the group of the activated panel:
        foreach (var pair in Panels.ToList())
            if (idOfPanelBeingActivated == pair.Value.Id)
                panelGroupIdOfPanelBeingActivated = pair.Value.PanelGroupId;

        // highlight the button of each panelGroup's focused panel...
        foreach (PanelGroup panelGroup in panelGroups)
        {
            // ...but only if not the panelGroup of the panel being activated:
            if (panelGroupIdOfPanelBeingActivated != panelGroup.Id)
            {
                int panelId = panelGroup.LocationPanelId;
                Panel panel = Panels[panelId];
                panel.ActivateButton();
            }
        }
    }
    public void HighlightLocationButton(int locationPanelId) 
        => Panels[locationPanelId].ActivateButton();
}
