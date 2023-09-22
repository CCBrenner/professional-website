using ProfessionalWebsite.Client.Services.UIService.Contracts;
using ProfessionalWebsite.Client.Services.UIService.Mgmt;
using ProfessionalWebsite.Client.Services.UIService.Mgmt.DataTables;

namespace ProfessionalWebsite.Client.Services.UIService;

public class UIService : IUIService
{
    public UIService()
    {
        var isContinuous = AnimationsTable.GetIsContinuous();
        AnimMgmt = new(isContinuous);

        NavMgmt = new();

        var PanelGroupsDictionary = PanelGroupsTable.GetPanelGroupsDict();
        var PanelsDictionary = PanelsTable.GetPanelsDict();
        PanelMgmt = new(PanelGroupsDictionary, PanelsDictionary);

        var SectionedPagesDictionary = SectionedPagesTable.GetSectionedPagesDict();
        var SectionsDictionary = SectionsTable.GetSectionsDict();
        SectionMgmt = new(SectionedPagesDictionary, SectionsDictionary);
    }

    public AnimMgmt AnimMgmt { get; private set; }
    public NavMgmt NavMgmt { get; private set; }
    public PanelMgmt PanelMgmt { get; private set; }
    public SectionMgmt SectionMgmt { get; private set; }

    /// <summary>
    /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    public void PlayAnimation(int animationIndex) =>
        AnimMgmt.PlayAnimation(animationIndex, PanelMgmt);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    /// <param name="isContinuous">Determines whether the animation should be played once or looped continuously.</param>
    public void PlayAnimation(int animationIndex, bool isContinuous) =>
        AnimMgmt.PlayAnimation(animationIndex, isContinuous, PanelMgmt);

    /// <summary>
    /// Stops continuous animation by chaning the animation class to blank (""); also hides the Discontinue button by the same means.
    /// </summary>
    public void DiscontinueAnimation() =>
        AnimMgmt.DiscontinueAnimation(PanelMgmt);

    /// <summary>
    /// Used to promote a section of a sectioned page that the user is navigating to. Navigation takes place based on the anchor element's href value (this method does not handle that navigation).
    /// </summary>
    /// <param name="sectionId">Id of the section to be promoted; it is located at the navigation destination page. This assumes the destination page is a sectioned page.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", this tells components that consume NavMgmt to update themselves because of a state change in NavMgmt. Components must subscribe to the event to receive update commands.</param>
    public void NavigateToSection(int sectionId, bool triggersOnPanelMgmtUpdated = true) =>
        NavMgmt.NavigateToSection(sectionId, PanelMgmt, SectionMgmt, triggersOnPanelMgmtUpdated);

    /// <summary>
    /// Updates the navigation highlights to show the proper location when navigating to a hard coded page. The only hard coded page at the time of writing is the original animations page which exists in the MainLayout component.
    /// </summary>
    /// <param name="panelId">Id of the panel whose button should be highlighted when navgiating to the hardcoded page.</param>
    public void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId) =>
        NavMgmt.NavigateToHardCodedPage(hardcodedPanelId, navGroupPanelId, PanelMgmt);

    /// <summary>
    /// Toggles a panel's state from "off" to "on" and vice versa by panel ID.
    /// </summary>
    /// <param name="selectedPanelId">ID of panel to be toggled on or off.</param>
    /// <returns></returns>
    public void TogglePanel(int selectedPanelId) =>
        PanelMgmt.TogglePanel(selectedPanelId);

    /// <summary>
    /// When navigating to a non-sectioned page (using an anchor element), deactivates all panels (including independent ones) and updates the location panel of the global navigation's panel group (leaving the location panel's button highlighted upon navgiation).
    /// </summary>
    /// <param name="panelId">ID of panel to be made location panel of global navigation panel group.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", causes components that consume PanelMgmt to update. Component must subscribe to the event to receive update commands from PanelMgmt.</param>
    public void UpdatePanelsWhenNavigating(int panelId, bool triggersOnPanelMgmtUpdated = true) =>
        PanelMgmt.UpdatePanelsWhenNavigating(panelId, triggersOnPanelMgmtUpdated);

    /// <summary>
    /// Collapses/Expands section based on section ID.
    /// </summary>
    /// <param name="sectionId">ID of section to be collapsed/expanded.</param>
    public void ToggleSection(int sectionId) =>
        SectionMgmt.ToggleSection(sectionId);

    /// <summary>
    /// Uses the SectionStatus to determine whether to expand all sections in the sectione page or to collapse all section in the sectinoed page.
    /// </summary>
    /// <param name="pageId">ID of sectioned page of which sections are being collapsed/expanded.</param>
    public void ToggleAllSections(int pageId) =>
        SectionMgmt.ToggleAllSections(pageId);
}
