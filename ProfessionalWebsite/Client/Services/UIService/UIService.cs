
namespace ProfessionalWebsite.Client.Services.UI;

public class UIService : IUIService
{
    public UIService(
        List<bool> animList,
        Dictionary<int, PanelGroup> panelGroups,
        Dictionary<int, Panel> panels,
        List<SectionedPage> sectionedPages,
        List<Section> sections)
    {
        int startingPanelId = 10;

        Animations = Animations.Create(string.Empty);
        IsContinuous = animList;
        PanelGroups = panelGroups;
        Panels = Panels.Create(panels);
        Sections = Sections.Create(sections, sectionedPages);

        Panels.SetBiDirectionalReferencesForPanelGroupsAndPanels(PanelGroups);

        Panels.Dictionary[startingPanelId].ActivateButton();
    }
    public Sections Sections { get; private set; }
    public bool IsCurrentPromo(int sectionId) => Sections.IsCurrentPromo(sectionId);
    public string SectionIsOpenCSS(int sectionId) => Sections.IsOpenCSS(sectionId);
    public bool SectionIsOpen(int sectionId) => Sections.Dictionary[sectionId].IsOpen;
    public void ToggleSection(int sectionId)
    {
        Sections.Dictionary[sectionId].Toggle();
        RaiseEventOnUiServiceChanged();
    }
    public string SectionName(int sectionId) => Sections.Dictionary[sectionId].Name;
    public bool ASectionIsCurrentlyPromo(int pageId) => Sections.ASectionIsCurrentlyPromo(pageId);
    public bool AllSectionsAreOpen(int pageId) => Sections.AllSectionsAreOpen(pageId);
    public string V2PanelIsActive(int panelId) => Panels.Dictionary[panelId].PanelStatus;
    public void ToggleAllSections(int pageId)
    {
        Sections.ToggleAllSections(pageId);
        RaiseEventOnUiServiceChanged();
    }
    public void PromoteSection(int sectionId)
    {
        Sections.PromoteSection(sectionId);
        RaiseEventOnUiServiceChanged();
    }
    public void ClickSidebarItem(int sectionId)
    {
        PromoteSection(sectionId);

        var pageId = Sections.Dictionary[sectionId].PageId;
        var pageLocPanelId = Sections.Pages[pageId].LocationPanelGroupId;
        Panels.HighlightLocationButton(pageLocPanelId);
    }
    public void PromoteSectionAndClosePanels(int sectionId)
    {
        Sections.PromoteSection(sectionId);
        Panels.DeactivateAllPanels();
        RaiseEventOnUiServiceChanged();
    }
    public void NavigateToPromotedSection(int sectionId)
    {
        PromoteSectionAndClosePanels(sectionId);

        var pageId = Sections.Dictionary[sectionId].PageId;
        var pageLocPanelId = Sections.Pages[pageId].LocationPanelGroupId;
        Panels.HighlightLocationButton(pageLocPanelId);
    }
    public void NavigateToSectionedPage(int idOfSectionedPageBeingLoaded)
    {
        Sections.OpenAllSections(idOfSectionedPageBeingLoaded);
        Panels.DeactivateAllPanels();

        var pageLocPanelId = Sections.Pages[idOfSectionedPageBeingLoaded].LocationPanelGroupId;
        Panels.HighlightLocationButton(pageLocPanelId);

        RaiseEventOnUiServiceChanged();
    }
    public bool SectionIsClosedAndThereIsNoPromo(int sectionId)
    {
        Section section = Sections.Dictionary[sectionId];
        return (!section.IsOpen
                &&
                !Sections.ASectionIsCurrentlyPromo(section.PageId));
    }
    public void V2ActivateLocationButtonsOfPanelGroups(int panelId)
        => Panels.ActivateLocationButtonsOfPanelGroups(panelId, PanelGroups.Values.ToList());
    //
    public List<bool> IsContinuous { get; private set; }
    public Animations Animations { get; private set; }
    public Panels Panels { get; private set; }
    public Dictionary<int, PanelGroup> PanelGroups { get; private set; }

    public event Action<string> OnUiServiceChanged;

    private void RaiseEventOnUiServiceChanged() => OnUiServiceChanged?.Invoke(string.Empty);
    /// <summary>
    /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    public void ToggleAnimation(int animationIndex)
    {
        Animations.ToggleAnimation(animationIndex, IsContinuous, Panels, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }
    public void ToggleOnePlayAnimation(int animationIndex)
    {
        Animations.ToggleOnePlayAnimation(animationIndex, Panels, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }
    public void ToggleContinuousAnimation(int animationIndex)
    {
        Animations.ToggleContinuousAnimation(animationIndex, Panels, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Stops continuous animation by chaning the animation class to blank (string.Empty); also hides the Discontinue button by the same means.
    /// </summary>
    public void DiscontinueAnimation()
    {
        Animations.DiscontinueAnimation(Panels, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Used to promote a section of a sectioned page that the user is navigating to. Navigation takes place based on the anchor element's href value (this method does not handle that navigation).
    /// </summary>
    /// <param name="sectionId">Id of the section to be promoted; it is located at the navigation destination page. This assumes the destination page is a sectioned page.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", this tells components that consume _nav to update themselves because of a state change in _nav. Components must subscribe to the event to receive update commands.</param>
    public void NavigateToSection(int sectionId)
    {
        Panels.DeactivateAllPanels();
        Panels.ActivateLocationButtonsOfGroups(PanelGroups);
        RaiseEventOnUiServiceChanged();
        NavMgmt.NavigateToSection(sectionId, Panels, PanelGroups, Sections);
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
        Panels.TogglePanel(selectedPanelId, PanelGroups);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// When navigating to a non-sectioned page (using an anchor element), deactivates all panels (including independent ones) and updates the location panel of the global navigation's panel group (leaving the location panel's button highlighted upon navgiation).
    /// </summary>
    /// <param name="panelId">ID of panel to be made location panel of global navigation panel group.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", causes components that consume _panel to update. Component must subscribe to the event to receive update commands from _panel.</param>
    public void UpdatePanelsWhenNavigating(int panelId)
    {
        Panels.DeactivateAllPanels();
        Panels.ActivateLocationButtonsOfGroups(PanelGroups);
        Panels.UpdateGroupLocationPanel(panelId, PanelGroups);
        RaiseEventOnUiServiceChanged();
    }
    public void DeactivateCooperativePanels()
    {
        Panels.DeactivateCooperativePanels();
        Panels.ActivateLocationButtonsOfGroups(PanelGroups);
        RaiseEventOnUiServiceChanged();
    }
    public void ActivatePanel(int selectedPanelId)
    {
        Panels.ActivatePanel(selectedPanelId, PanelGroups.Values.ToList());
        RaiseEventOnUiServiceChanged();
    }
    public void DeactivatePanel(int selectedPanelId)
    {
        Panels.DeactivatePanel(selectedPanelId);
        RaiseEventOnUiServiceChanged();
    }
    public static UIService Create(
        List<bool> animList, 
        Dictionary<int, PanelGroup> panelGroups, 
        Dictionary<int, Panel> panels, 
        List<SectionedPage> sectionedPages, 
        List<Section> sections)
    {
        return new(animList, panelGroups, panels, sectionedPages, sections);
    }
}
