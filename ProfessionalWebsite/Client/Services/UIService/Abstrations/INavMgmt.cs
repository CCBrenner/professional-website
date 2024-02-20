namespace ProfessionalWebsite.Client.Services.UI;

public interface INavMgmt
{
    void NavigateToSection(Sec sectionId, IPanelMgmt panelMgmt, ISectionMgmt sectionMgmt);
    void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId, IPanelMgmt panelMgmt);
}
