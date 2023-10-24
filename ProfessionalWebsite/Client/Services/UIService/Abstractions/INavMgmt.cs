namespace ProfessionalWebsite.Client.Services.UI;

public interface INavMgmt
{
    void NavigateToSection(
        int sectionId,
        IPanelMgmt panelMgmt, 
        SectionMgmt sectionMgmt, 
        Dictionary<int, Panel> panels, 
        Dictionary<int, PanelGroup> panelGroups);
    void NavigateToHardCodedPage(
        int hardcodedPanelId, 
        int navGroupPanelId, 
        IPanelMgmt panelMgmt, 
        Dictionary<int, Panel> panels, 
        Dictionary<int, PanelGroup> panelGroups);
}
