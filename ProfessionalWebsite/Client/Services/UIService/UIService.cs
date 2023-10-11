namespace ProfessionalWebsite.Client.Services.UI;

public class UIService : IUIService
{
    public UIService()
    {
        IsContinuous = AnimationsTable.GetIsContinuous();
        _anim = new(IsContinuous);

        Nav = new();

        var PanelGroupsDictionary = PanelGroupsTable.GetPanelGroupsDict();
        var PanelsDictionary = PanelsTable.GetPanelsDict();
        Panel = new(PanelGroupsDictionary, PanelsDictionary);

        var SectionedPagesDictionary = SectionedPagesTable.GetSectionedPagesDict();
        var SectionsDictionary = SectionsTable.GetSectionsDict();
        Section = new(SectionedPagesDictionary, SectionsDictionary);
    }

    private AnimMgmt _anim;
    public NavMgmt Nav { get; private set; }
    public PanelMgmt Panel { get; private set; }
    public SectionMgmt Section { get; private set; }
    public string AnimateMain => _anim.AnimateMain;
    public List<bool> IsContinuous { get; private set; }
    public event Action<string> OnUiServiceChanged;

    /// <summary>
    /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    public void PlayAnimation(int animationIndex) =>
        _anim.PlayAnimation(animationIndex, Panel);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    /// <param name="isContinuous">Determines whether the animation should be played once or looped continuously.</param>
    public void PlayAnimation(int animationIndex, bool isContinuous)
    {
        _anim.PlayAnimation(animationIndex, isContinuous, Panel);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Stops continuous animation by chaning the animation class to blank (""); also hides the Discontinue button by the same means.
    /// </summary>
    public void DiscontinueAnimation() =>
        _anim.DiscontinueAnimation(Panel);

    /// <summary>
    /// Used to promote a section of a sectioned page that the user is navigating to. Navigation takes place based on the anchor element's href value (this method does not handle that navigation).
    /// </summary>
    /// <param name="sectionId">Id of the section to be promoted; it is located at the navigation destination page. This assumes the destination page is a sectioned page.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", this tells components that consume Nav to update themselves because of a state change in Nav. Components must subscribe to the event to receive update commands.</param>
    public void NavigateToSection(int sectionId, bool triggersOnPanelMgmtUpdated = true) =>
        Nav.NavigateToSection(sectionId, Panel, Section, triggersOnPanelMgmtUpdated);

    /// <summary>
    /// Updates the navigation highlights to show the proper location when navigating to a hard coded page. The only hard coded page at the time of writing is the original animations page which exists in the MainLayout component.
    /// </summary>
    /// <param name="panelId">Id of the panel whose button should be highlighted when navgiating to the hardcoded page.</param>
    public void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId) =>
        Nav.NavigateToHardCodedPage(hardcodedPanelId, navGroupPanelId, Panel);

    /// <summary>
    /// Toggles a panel's state from "off" to "on" and vice versa by panel ID.
    /// </summary>
    /// <param name="selectedPanelId">ID of panel to be toggled on or off.</param>
    /// <returns></returns>
    public void TogglePanel(int selectedPanelId) =>
        Panel.TogglePanel(selectedPanelId);

    /// <summary>
    /// When navigating to a non-sectioned page (using an anchor element), deactivates all panels (including independent ones) and updates the location panel of the global navigation's panel group (leaving the location panel's button highlighted upon navgiation).
    /// </summary>
    /// <param name="panelId">ID of panel to be made location panel of global navigation panel group.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", causes components that consume Panel to update. Component must subscribe to the event to receive update commands from Panel.</param>
    public void UpdatePanelsWhenNavigating(int panelId, bool triggersOnPanelMgmtUpdated = true) =>
        Panel.UpdatePanelsWhenNavigating(panelId, triggersOnPanelMgmtUpdated);

    /// <summary>
    /// Collapses/Expands section based on section ID.
    /// </summary>
    /// <param name="sectionId">ID of section to be collapsed/expanded.</param>
    public void ToggleSection(int sectionId) =>
        Section.ToggleSection(sectionId);

    /// <summary>
    /// Uses the SectionStatus to determine whether to expand all sections in the sectione page or to collapse all section in the sectinoed page.
    /// </summary>
    /// <param name="pageId">ID of sectioned page of which sections are being collapsed/expanded.</param>
    public void ToggleAllSections(int pageId) =>
        Section.ToggleAllSections(pageId);
    private void RaiseEventOnUiServiceChanged() => OnUiServiceChanged?.Invoke("");

}
