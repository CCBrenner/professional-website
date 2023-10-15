namespace ProfessionalWebsite.Client.Services.UI;

public class UIService : IUIService
{
    public UIService()
    {
        IsContinuous = AnimationsTable.GetIsContinuous();
        _anim = AnimMgmt.Create(IsContinuous);

        _nav = NavMgmt.Create();

        PanelGroups = PanelGroupsTable.GetPanelGroupsDict();
        Panels = PanelsTable.GetPanelsDict();
        _panel = PanelMgmt.Create(PanelGroups, Panels);

        SectionedPages = SectionedPagesTable.GetSectionedPagesDict();
        Sections = SectionsTable.GetSectionsDict();
        _section = SectionMgmt.Create(SectionedPages, Sections);
    }

    private AnimMgmt _anim;
    private NavMgmt _nav;
    private PanelMgmt _panel;
    private SectionMgmt _section;
    public string AnimateMain => _anim.AnimateMain;
    public List<bool> IsContinuous { get; private set; }
    public Dictionary<int, Panel> Panels { get; private set; }
    public Dictionary<int, PanelGroup> PanelGroups { get; private set; }
    public Dictionary<int, Section> Sections { get; private set; }
    public Dictionary<int, SectionedPage> SectionedPages { get; private set; }

    public event Action<string> OnUiServiceChanged;

    /// <summary>
    /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    public void PlayAnimation(int animationIndex)
    {
        _anim.PlayAnimation(animationIndex, _panel);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    /// <param name="isContinuous">Determines whether the animation should be played once or looped continuously.</param>
    public void PlayAnimation(int animationIndex, bool isContinuous)
    {
        _anim.PlayAnimation(animationIndex, isContinuous, _panel);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Stops continuous animation by chaning the animation class to blank (""); also hides the Discontinue button by the same means.
    /// </summary>
    public void DiscontinueAnimation() =>
        _anim.DiscontinueAnimation(_panel);

    /// <summary>
    /// Used to promote a section of a sectioned page that the user is navigating to. Navigation takes place based on the anchor element's href value (this method does not handle that navigation).
    /// </summary>
    /// <param name="sectionId">Id of the section to be promoted; it is located at the navigation destination page. This assumes the destination page is a sectioned page.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", this tells components that consume _nav to update themselves because of a state change in _nav. Components must subscribe to the event to receive update commands.</param>
    public void NavigateToSection(int sectionId)
    {
        DeactivatePanels(true);
        _nav.NavigateToSection(sectionId, _panel, _section);
    }

    /// <summary>
    /// Updates the navigation highlights to show the proper location when navigating to a hard coded page. The only hard coded page at the time of writing is the original animations page which exists in the MainLayout component.
    /// </summary>
    /// <param name="panelId">Id of the panel whose button should be highlighted when navgiating to the hardcoded page.</param>
    public void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId) =>
        _nav.NavigateToHardCodedPage(hardcodedPanelId, navGroupPanelId, _panel);

    /// <summary>
    /// Toggles a panel's state from "off" to "on" and vice versa by panel ID.
    /// </summary>
    /// <param name="selectedPanelId">ID of panel to be toggled on or off.</param>
    /// <returns></returns>
    public void TogglePanel(int selectedPanelId)
    {
        _panel.TogglePanel(selectedPanelId);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// When navigating to a non-sectioned page (using an anchor element), deactivates all panels (including independent ones) and updates the location panel of the global navigation's panel group (leaving the location panel's button highlighted upon navgiation).
    /// </summary>
    /// <param name="panelId">ID of panel to be made location panel of global navigation panel group.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", causes components that consume _panel to update. Component must subscribe to the event to receive update commands from _panel.</param>
    public void UpdatePanelsWhenNavigating(int panelId)
    {
        DeactivatePanels(true);
        _panel.UpdateGroupLocationPanel(panelId);

        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Collapses/Expands section based on section ID.
    /// </summary>
    /// <param name="sectionId">ID of section to be collapsed/expanded.</param>
    public void ToggleSection(int sectionId)
    {
        _section.ToggleSection(sectionId);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Uses the SectionStatus to determine whether to expand all sections in the sectione page or to collapse all section in the sectinoed page.
    /// </summary>
    /// <param name="pageId">ID of sectioned page of which sections are being collapsed/expanded.</param>
    public void ToggleAllSections(int pageId)
    {
        _section.ToggleAllSections(pageId);
        RaiseEventOnUiServiceChanged();
    }
    private void RaiseEventOnUiServiceChanged() => OnUiServiceChanged?.Invoke("");
    public void DeactivateAllPanels(
        bool setActivePanelGroupToLocationPanel
    )
    {
        _panel.DeactivateAllPanels(setActivePanelGroupToLocationPanel, false);
        RaiseEventOnUiServiceChanged();
    }
    public void DeactivatePanels(
        bool setActivePanelGroupToLocationPanel
    )
    {
        _panel.DeactivateAllPanels(setActivePanelGroupToLocationPanel, true);
        RaiseEventOnUiServiceChanged();
    }
    public void ActivatePanel(int selectedPanelId)
    {
        _panel.ActivatePanel(selectedPanelId);
        RaiseEventOnUiServiceChanged();
    }
    public void DeactivatePanel(int selectedPanelId)
    {
        _panel.DeactivatePanel(selectedPanelId);
        RaiseEventOnUiServiceChanged();
    }
    public void UpdateGroupLocationPanel(int panelId)
    {
        _panel.UpdateGroupLocationPanel(panelId);
        RaiseEventOnUiServiceChanged();
    }
}
