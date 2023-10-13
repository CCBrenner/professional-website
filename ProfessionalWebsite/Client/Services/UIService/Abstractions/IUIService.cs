namespace ProfessionalWebsite.Client.Services.UI;

public interface IUIService
{
    string AnimateMain { get; }
    List<bool> IsContinuous { get; }
    Dictionary<int, Panel> Panels { get; }
    Dictionary<int, PanelGroup> PanelGroups { get; }
    public Dictionary<int, Section> Sections { get; }
    public Dictionary<int, SectionedPage> SectionedPages { get; }
    void ActivatePanel(int selectedPanelId);
    void DeactivatePanel(int selectedPanelId);
    void UpdateGroupLocationPanel(int panelId);
}
