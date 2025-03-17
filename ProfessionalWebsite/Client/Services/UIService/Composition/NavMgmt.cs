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
        Panels panels, 
        Dictionary<int, PanelGroup> panelGroups, 
        V2Sections sections)
    {
        sections.PromoteSection(sectionId);
        int locationPanelGroupId = sections.GetLocationPanelGroupId(sectionId);
        if (locationPanelGroupId < 0) return;
        panels.UpdateGroupLocationPanel(locationPanelGroupId, panelGroups);
    }
    public static void NavigateToHardCodedPage(
        int hardcodedPanelId, 
        int navGroupPanelId, 
        Panels panels, 
        Dictionary<int, PanelGroup> panelGroups)
    {
        panels.UpdateGroupLocationPanel(navGroupPanelId, panelGroups);
        panels.ActivatePanel(hardcodedPanelId, panelGroups.Values.ToList());
    }
}
