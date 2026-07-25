namespace ProfessionalWebsite.Client.Services.UI;

public interface IUIService
{
    List<bool> IsContinuous { get; }
    PanelService PanelService { get; }
    public SectionService SectionService { get; }
    void ActivatePanel(int selectedPanelId);
    void DeactivatePanel(int selectedPanelId);
}
