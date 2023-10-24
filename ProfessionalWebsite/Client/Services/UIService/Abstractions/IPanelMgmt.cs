namespace ProfessionalWebsite.Client.Services.UI;

public interface IPanelMgmt
{
    void TogglePanel(int selectedPanelId, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups);
    void UpdateGroupLocationPanel(int panelId, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups);
    void ActivatePanel(int selectedPanelId, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
    void ActivateLocationButtonsOfGroups(Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups);
    void DeactivateAllPanels(IEnumerable<Panel> panels);
    void DeactivatePanel(int selectedPanelId, Dictionary<int, Panel> panels);
    void DeactivateCooperativePanels(List<Panel> panels);
}
