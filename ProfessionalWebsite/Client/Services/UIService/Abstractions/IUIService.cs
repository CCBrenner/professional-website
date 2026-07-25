namespace ProfessionalWebsite.Client.Services.UI;

public interface IUIService
{
    PanelService PanelService { get; }
    public SectionService SectionService { get; }
    void ActivatePanel(int selectedPanelId);
    void DeactivatePanel(int selectedPanelId);
}
