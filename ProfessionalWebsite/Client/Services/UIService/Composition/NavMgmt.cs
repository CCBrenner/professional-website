namespace ProfessionalWebsite.Client.Services.UI;

public static class NavMgmt
{
    /*
    Definitions:
        - "sectioned page" : a page that implements according sections (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sections
        - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sections of the page
    */
    public static void NavigateToSection(int sectionId, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        try
        {
            SectionMgmt.CollapseAllShowOne(sectionId, sections, sectionedPages);
            int locationPanelGroupId = SectionMgmt.GetLocationPanelGroupId(sectionId, sections, sectionedPages);
            if (locationPanelGroupId < 0) return;
            PanelMgmt.UpdateGroupLocationPanel(locationPanelGroupId, panels, panelGroups);
        }
        catch (NullReferenceException nrEx)
        {
            Console.WriteLine(nrEx.Message + nrEx.StackTrace);
        }
    }
    public static void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        PanelMgmt.UpdateGroupLocationPanel(navGroupPanelId, panels, panelGroups);
        PanelMgmt.ActivatePanel(hardcodedPanelId, panels, panelGroups.Values.ToList());
    }
}
