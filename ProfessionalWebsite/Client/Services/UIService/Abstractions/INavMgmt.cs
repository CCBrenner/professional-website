namespace ProfessionalWebsite.Client.Services.UI;

public interface INavMgmt
{
    void NavigateToSection(
        int sectionId,
        IPanelMgmt panelMgmt, 
        ISectionMgmt sectionMgmt, 
        Dictionary<int, Panel> panels, 
        Dictionary<int, PanelGroup> panelGroups, 
        Dictionary<int, Section> sections, 
        Dictionary<int, SectionedPage> sectionedPages);
    void NavigateToHardCodedPage(
        int hardcodedPanelId, 
        int navGroupPanelId, 
        IPanelMgmt panelMgmt, 
        Dictionary<int, Panel> panels, 
        Dictionary<int, PanelGroup> panelGroups);
}
