namespace ProfessionalWebsite.Client.Services.UI;

public static class NavMgmt
{
    /*
    Definitions:
        - "sectioned page" : a page that implements according sectionsList (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sectionsList
        - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sectionsList of the page
    */
    public static void NavigateToSection(
        int sectionId, 
        PanelService panelService, 
        SectionService sections)
    {
        sections.PromoteSection(sectionId);
        int locationPanelGroupId = sections.GetLocationPanelGroupId(sectionId);
        if (locationPanelGroupId < 0) return;
        panelService.UpdateGroupLocationPanel(locationPanelGroupId);
    }
    public static void NavigateToHardCodedPage(
        int hardcodedPanelId, 
        int navGroupPanelId, 
        PanelService panelService)
    {
        panelService.UpdateGroupLocationPanel(navGroupPanelId);
        panelService.ActivatePanel(hardcodedPanelId);
    }
}
