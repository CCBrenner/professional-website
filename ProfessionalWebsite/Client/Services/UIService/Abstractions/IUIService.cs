namespace ProfessionalWebsite.Client.Services.UI;

public interface IUIService
{
    SectionMgmt Section { get; }
    string AnimateMain { get; }
    List<bool> IsContinuous { get; }
    Dictionary<int, Panel> Panels { get; }
    Dictionary<int, PanelGroup> PanelGroups { get; }
    void ActivatePanel(int selectedPanelId);
    void DeactivatePanel(int selectedPanelId);
    void UpdateGroupLocationPanel(int panelId);
}
