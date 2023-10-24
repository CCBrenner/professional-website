namespace ProfessionalWebsite.Client.Services.UI;

public interface INavMgmt
{
    void NavigateToSection(int sectionId, PanelMgmt panelMgmt, SectionMgmt sectionMgmt);
    void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId, PanelMgmt panelMgmt);
}
