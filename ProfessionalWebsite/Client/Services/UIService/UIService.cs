namespace ProfessionalWebsite.Client.Services.UI;

public class UIService : IUIService
{
    private UIService(
        List<bool> isContinuous, 
        Dictionary<int, PanelGroup> panelGroups, 
        Dictionary<int, Panel> panels, 
        Dictionary<int, SectionedPage> sectionedPages, 
        Dictionary<int, Section> sections)
    {
        AnimateMain = string.Empty;
        IsContinuous = isContinuous;
        PanelGroups = panelGroups;
        Panels = panels;
        SectionedPages = sectionedPages;
        Sections = sections;

        PanelMgmt.SetBiDirectionalReferencesForPanelGroupsAndPanels(Panels, PanelGroups);
        SectionMgmt.SetBiDirectionalReferencesForSectionedPagesAndSections(Sections, SectionedPages);
    }
    public string AnimateMain { get; private set; }
    public List<bool> IsContinuous { get; private set; }
    public Dictionary<int, Panel> Panels { get; private set; }
    public Dictionary<int, PanelGroup> PanelGroups { get; private set; }
    public Dictionary<int, Section> Sections { get; private set; }
    public Dictionary<int, SectionedPage> SectionedPages { get; private set; }

    public event Action<string> OnUiServiceChanged;
    public static UIService Create(
        List<bool> isContinuous,
        Dictionary<int, PanelGroup> panelGroups,
        Dictionary<int, Panel> panels,
        Dictionary<int, SectionedPage> sectionedPages,
        Dictionary<int, Section> sections) => 
        new(isContinuous, panelGroups, panels, sectionedPages, sections);

    private void RaiseEventOnUiServiceChanged() => OnUiServiceChanged?.Invoke(string.Empty);
    /// <summary>
    /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    public void ToggleAnimation(int animationIndex)
    {
        AnimateMain = AnimMgmt.ToggleAnimation(animationIndex, AnimateMain, IsContinuous, Panels, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }
    public void ToggleOnePlayAnimation(int animationIndex)
    {
        AnimateMain = AnimMgmt.ToggleOnePlayAnimation(animationIndex, AnimateMain, Panels, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }
    public void ToggleContinuousAnimation(int animationIndex)
    {
        AnimateMain = AnimMgmt.ToggleContinuousAnimation(animationIndex, AnimateMain, Panels, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Stops continuous animation by chaning the animation class to blank (string.Empty); also hides the Discontinue button by the same means.
    /// </summary>
    public void DiscontinueAnimation() =>
        AnimateMain = AnimMgmt.DiscontinueAnimation(Panels, PanelGroups.Values.ToList());

    /// <summary>
    /// Used to promote a section of a sectioned page that the user is navigating to. Navigation takes place based on the anchor element's href value (this method does not handle that navigation).
    /// </summary>
    /// <param name="sectionId">Id of the section to be promoted; it is located at the navigation destination page. This assumes the destination page is a sectioned page.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", this tells components that consume _nav to update themselves because of a state change in _nav. Components must subscribe to the event to receive update commands.</param>
    public void NavigateToSection(int sectionId)
    {
        DeactivateAllPanels();
        NavMgmt.NavigateToSection(sectionId, Panels, PanelGroups, Sections, SectionedPages);
    }

    /// <summary>
    /// Updates the navigation highlights to show the proper location when navigating to a hard coded page. The only hard coded page at the time of writing is the original animations page which exists in the MainLayout component.
    /// </summary>
    /// <param name="panelId">Id of the panel whose button should be highlighted when navgiating to the hardcoded page.</param>
    public void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId) =>
        NavMgmt.NavigateToHardCodedPage(hardcodedPanelId, navGroupPanelId, Panels, PanelGroups);

    /// <summary>
    /// Toggles a panel's state from "off" to "on" and vice versa by panel ID.
    /// </summary>
    /// <param name="selectedPanelId">ID of panel to be toggled on or off.</param>
    /// <returns></returns>
    public void TogglePanel(int selectedPanelId)
    {
        PanelMgmt.TogglePanel(selectedPanelId, Panels, PanelGroups);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// When navigating to a non-sectioned page (using an anchor element), deactivates all panels (including independent ones) and updates the location panel of the global navigation's panel group (leaving the location panel's button highlighted upon navgiation).
    /// </summary>
    /// <param name="panelId">ID of panel to be made location panel of global navigation panel group.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", causes components that consume _panel to update. Component must subscribe to the event to receive update commands from _panel.</param>
    public void UpdatePanelsWhenNavigating(int panelId)
    {
        DeactivateAllPanels();
        PanelMgmt.UpdateGroupLocationPanel(panelId, Panels, PanelGroups);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Collapses/Expands section based on section ID.
    /// </summary>
    /// <param name="sectionId">ID of section to be collapsed/expanded.</param>
    public void ToggleSection(int sectionId)
    {
        SectionMgmt.ToggleSection(sectionId, Sections, SectionedPages);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Uses the SectionStatus to determine whether to expand all sections in the sectione page or to collapse all section in the sectinoed page.
    /// </summary>
    /// <param name="pageId">ID of sectioned page of which sections are being collapsed/expanded.</param>
    public void ToggleSectionedPage(int pageId)
    {
        var page = SectionedPages[pageId];
        page.Toggle();
        RaiseEventOnUiServiceChanged();
    }
    public void DeactivateCooperativePanels()
    {
        PanelMgmt.DeactivateCooperativePanels(Panels.Values.ToList());
        PanelMgmt.ActivateLocationButtonsOfGroups(Panels, PanelGroups);
        RaiseEventOnUiServiceChanged();
    }
    public void ActivatePanel(int selectedPanelId)
    {
        PanelMgmt.ActivatePanel(selectedPanelId, Panels, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }
    public void DeactivatePanel(int selectedPanelId)
    {
        PanelMgmt.DeactivatePanel(selectedPanelId, Panels);
        RaiseEventOnUiServiceChanged();
    }
    private void DeactivateAllPanels()
    {
        PanelMgmt.DeactivateAllPanels(Panels.Values.ToList());
        PanelMgmt.ActivateLocationButtonsOfGroups(Panels, PanelGroups);
        RaiseEventOnUiServiceChanged();
    }
}
