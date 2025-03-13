namespace ProfessionalWebsite.Client.Services.UI;

public interface IUIService
{
    List<bool> IsContinuous { get; }
    Panels Panels { get; }
    Dictionary<int, PanelGroup> PanelGroups { get; }
    public Sections Sections { get; }
    public Dictionary<int, SectionedPage> SectionedPages { get; }
    void ActivatePanel(int selectedPanelId);
    void DeactivatePanel(int selectedPanelId);
}
