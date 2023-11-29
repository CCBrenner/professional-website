namespace ProfessionalWebsite.Client.Services.UI;

public class NavMgmt : INavMgmt
{
    /*
    Definitions:
        - "sectioned page" : a page that implements according sections (collapse/expand) & utilizes SectionsMgmt for the handling logic of those sections
        - "promoting" : [concerning a section in a sectioned page] expanding it, move it to the top of the page, and collapsing all other sections of the page
    */
    public static NavMgmt Create() => new();
    public void NavigateToSection(int sectionId, IPanelMgmt panelMgmt, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups, Dictionary<int, Section> sections, Dictionary<int, SectionedPage> sectionedPages)
    {
        try
        {
            SectionMgmt.CollapseAllShowOne(sectionId, sections, sectionedPages);
            int locationPanelGroupId = SectionMgmt.GetLocationPanelGroupId(sectionId, sections, sectionedPages);
            if (locationPanelGroupId != -1)
                panelMgmt.UpdateGroupLocationPanel(locationPanelGroupId, panels, panelGroups);
        }
        catch (NullReferenceException nrEx)
        {
            Console.WriteLine(nrEx.Message + nrEx.StackTrace);
        }
    }
    public void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId, IPanelMgmt panelMgmt, Dictionary<int, Panel> panels, Dictionary<int, PanelGroup> panelGroups)
    {
        panelMgmt.UpdateGroupLocationPanel(navGroupPanelId, panels, panelGroups);
        panelMgmt.ActivatePanel(hardcodedPanelId, panels, panelGroups.Values.ToList());
    }
}
