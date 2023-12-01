namespace ProfessionalWebsite.Client.Services.UI;

public interface INavMgmt
{
    void NavigateToSection(
        int sectionId, 
        Dictionary<int, Panel> panels, 
        Dictionary<int, PanelGroup> panelGroups, 
        Dictionary<int, Section> sections, 
        Dictionary<int, SectionedPage> sectionedPages);
    void NavigateToHardCodedPage(
        int hardcodedPanelId, 
        int navGroupPanelId, 
        Dictionary<int, Panel> panels, 
        Dictionary<int, PanelGroup> panelGroups);
}
