
namespace ProfessionalWebsite.Client.Services.UI;

public class Panels
{
    /*
    Definitions:
        - "cooperative" vs. "independent" panels: "cooperative" panels are panels that can only ever be "on" if all other cooperative panels are turned "off". "Independent" panels can stay on while a cooperative panel is on as well as when all cooperative panels are turned off. Overrides do exist for behavior of each, but defaults reflect what is described above.
    */
    private Panels(Dictionary<int, Panel> initPanels)
    {
        Dictionary = initPanels;
    }
    public static Panels Create(Dictionary<int, Panel> initPanels) => new Panels(initPanels);
    public Dictionary<int, Panel> Dictionary { get; private set; }
    public void DeactivateAllPanels()
    {
        foreach (var pair in Dictionary.ToList())
        {
            pair.Value.Deactivate();
        }
    }
    public void DeactivateCooperativePanels()
    {
        foreach (var pair in Dictionary.ToList())
        {
            if (pair.Value.IsCooperativePanel)
            {
                pair.Value.Deactivate();
            }
        }
    }
    public void DeactivatePanel(int selectedPanelId)
    {
        Dictionary[selectedPanelId].Deactivate();
    }
    public void ActivatePanel(int selectedPanelId, List<PanelGroup> panelGroupsList)
    {
        var panelsList = Dictionary.Values.ToList();
        DeactivateCooperativePanels();
        ActivateLocationButtonsOfPanelGroups(selectedPanelId, panelGroupsList);
        Dictionary[selectedPanelId].Activate();
    }
    public void TogglePanel(int selectedPanelId, Dictionary<int, PanelGroup> panelGroups)
    {
        if (Dictionary[selectedPanelId].PanelStatus == string.Empty)
        {
            DeactivateCooperativePanels();
            ActivateLocationButtonsOfPanelGroups(selectedPanelId, panelGroups.Values.ToList());
            Dictionary[selectedPanelId].Activate();
        }
        else
        {
            Dictionary[selectedPanelId].Deactivate();
            DeactivateCooperativePanels();
            ActivateLocationButtonsOfGroups(panelGroups);
        }
    }
    public void UpdateGroupLocationPanel(int panelId, Dictionary<int, PanelGroup> panelGroups)
    {
        int pgId = Dictionary[panelId].PanelGroupId;
        if (pgId < 0) return;  // will be -1 if independent panel (has no specificed group)
        int lpId = panelGroups[pgId].LocationPanelId;
        Dictionary[lpId].Deactivate();
        panelGroups[pgId].LocationPanelId = panelId;
        Dictionary[panelId].ActivateButton();
    }
    public void ActivateLocationButtonsOfGroups(Dictionary<int, PanelGroup> panelGroups)
    {
        foreach (PanelGroup panelGroup in panelGroups.Values)
        {
            int panelId = panelGroup.LocationPanelId;
            Panel panel = Dictionary[panelId];
            panel.ActivateButton();
        }
    }

    public void SetBiDirectionalReferencesForPanelGroupsAndPanels(Dictionary<int, PanelGroup> panelGroups)
    {
        foreach (var panel in Dictionary.Values)
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
    public void ActivateLocationButtonsOfPanelGroups(int idOfPanelBeingActivated, List<PanelGroup> panelGroups)
    {
        // use this to determine get the group of the deactivated panel:
        int panelGroupIdOfPanelBeingActivated = -1;

        // get the group of the activated panel:
        foreach (var pair in Dictionary.ToList())
            if (idOfPanelBeingActivated == pair.Value.Id)
                panelGroupIdOfPanelBeingActivated = pair.Value.PanelGroupId;

        // highlight the button of each panelGroup's focused panel...
        foreach (PanelGroup panelGroup in panelGroups)
        {
            // ...but only if not the panelGroup of the panel being activated:
            if (panelGroupIdOfPanelBeingActivated != panelGroup.Id)
            {
                int panelId = panelGroup.LocationPanelId;
                Panel panel = Dictionary[panelId];
                panel.ActivateButton();
            }
        }
    }
    public void HighlightLocationButton(int locationPanelId) 
        => Dictionary[locationPanelId].ActivateButton();
}
