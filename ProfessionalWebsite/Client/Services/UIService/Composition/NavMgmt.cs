namespace ProfessionalWebsite.Client.Services.UI;

public static class NavMgmt
{
    /*
    Definitions:
        - "sectioned page" : a page that implements according sections (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sections
        - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sections of the page
    */
    public static void NavigateToSection(int sectionId, Panels panels, Dictionary<int, PanelGroup> panelGroups, Sections sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        sections.CollapseAllShowOne(sectionId, sectionedPages);
        int locationPanelGroupId = sections.GetLocationPanelGroupId(sectionId, sectionedPages);
        if (locationPanelGroupId < 0) return;
        panels.UpdateGroupLocationPanel(locationPanelGroupId, panelGroups);
    }
    public static void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId, Panels panels, Dictionary<int, PanelGroup> panelGroups)
    {
        panels.UpdateGroupLocationPanel(navGroupPanelId, panelGroups);
        panels.ActivatePanel(hardcodedPanelId, panelGroups.Values.ToList());
    }
}
