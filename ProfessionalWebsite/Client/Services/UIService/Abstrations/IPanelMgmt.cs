namespace ProfessionalWebsite.Client.Services.UI;

public interface IPanelMgmt
{
    Dictionary<int, Panel> Panel { get; }
    void DeactivatePanel(int selectedPanelId);
    void TogglePanel(int selectedPanelId);
    void UpdatePanelsWhenNavigating(int panelId);
    void ActivatePanel(int selectedPanelId);
    void UpdateGroupLocationPanel(int panelId);
    void DeactivateAllPanels(
        bool setActivePanelGroupToLocationPanel,
        bool includeIndependentPanels = false
    );
}
