
namespace ProfessionalWebsite.Client.Services.UI;

public class UIService : IUIService
{
    public UIService()
    {
        int startingPanelId = 10;
        PanelService = PanelService.Create(startingPanelId);
        AnimationService = AnimationService.Create();
        SectionService = SectionService.Create();
    }
    public static UIService Create()
    {
        return new();
    }

    public event Action<string> OnUiServiceChanged;
    public SectionService SectionService { get; private set; }
    public AnimationService AnimationService { get; private set; }
    public PanelService PanelService { get; private set; }

    private void RaiseEventOnUiServiceChanged()
    {
        OnUiServiceChanged?.Invoke(string.Empty);
    }
    public string SectionIsOpenCSS(int sectionId)
    {
        return SectionService.IsOpenCSS(sectionId);
    }
    public bool SectionIsOpen(int sectionId)
    {
        return SectionService.Dictionary[sectionId].IsOpen;
    }
    public void ToggleSection(int sectionId)
    {
        SectionService.Dictionary[sectionId].Toggle();
        RaiseEventOnUiServiceChanged();
    }
    public string SectionName(int sectionId)
    {
        return SectionService.Dictionary[sectionId].Name;
    }
    public bool ASectionIsCurrentlyPromo(int pageId)
    {
        return SectionService.ASectionIsCurrentlyPromo(pageId);
    }
    public bool AllSectionsAreOpen(int pageId)
    {
        return SectionService.AllSectionsAreOpen(pageId);
    }
    public string V2PanelIsActive(int panelId)
    {
        return PanelService.Panels[panelId].PanelStatus;
    }
    public void ToggleAllSections(int pageId)
    {
        SectionService.ToggleAllSections(pageId);
        RaiseEventOnUiServiceChanged();
    }
    public void PromoteSection(int sectionId)
    {
        SectionService.PromoteSection(sectionId);
        RaiseEventOnUiServiceChanged();
    }
    public void ClickSidebarItem(int sectionId)
    {
        PromoteSection(sectionId);

        var pageId = SectionService.Dictionary[sectionId].PageId;
        var pageLocPanelId = SectionService.Pages[pageId].LocationPanelGroupId;
        PanelService.HighlightLocationButton(pageLocPanelId);
    }
    public void PromoteSectionAndClosePanels(int sectionId)
    {
        SectionService.PromoteSection(sectionId);
        PanelService.DeactivateAllPanels();
        RaiseEventOnUiServiceChanged();
    }
    public void NavigateToPromotedSection(int sectionId)
    {
        PromoteSectionAndClosePanels(sectionId);

        var pageId = SectionService.Dictionary[sectionId].PageId;
        var pageLocPanelId = SectionService.Pages[pageId].LocationPanelGroupId;
        PanelService.HighlightLocationButton(pageLocPanelId);
    }
    public void NavigateToSectionedPage(int idOfSectionedPageBeingLoaded)
    {
        SectionService.OpenAllSections(idOfSectionedPageBeingLoaded);
        PanelService.DeactivateAllPanels();

        var pageLocPanelId = SectionService.Pages[idOfSectionedPageBeingLoaded].LocationPanelGroupId;
        PanelService.HighlightLocationButton(pageLocPanelId);

        RaiseEventOnUiServiceChanged();
    }
    public bool SectionIsClosedAndThereIsNoPromo(int sectionId)
    {
        Section section = SectionService.Dictionary[sectionId];
        return (!section.IsOpen
                &&
                !SectionService.ASectionIsCurrentlyPromo(section.PageId));
    }
    public void ToggleOnePlayAnimation(int animationIndex)
    {
        AnimationService.ToggleOnePlayAnimation(animationIndex, PanelService);
        RaiseEventOnUiServiceChanged();
    }
    public void ToggleContinuousAnimation(int animationIndex)
    {
        AnimationService.ToggleContinuousAnimation(animationIndex, PanelService);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Stops continuous animation by chaning the animation class to blank (string.Empty); also hides the Discontinue button by the same means.
    /// </summary>
    public void DiscontinueAnimation()
    {
        AnimationService.DiscontinueAnimation(PanelService);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// Toggles a panel's state from "off" to "on" and vice versa by panel ID.
    /// </summary>
    /// <param name="selectedPanelId">ID of panel to be toggled on or off.</param>
    /// <returns></returns>
    public void TogglePanel(int selectedPanelId)
    {
        PanelService.TogglePanel(selectedPanelId);
        RaiseEventOnUiServiceChanged();
    }

    /// <summary>
    /// When navigating to a non-sectioned page (using an anchor element), deactivates all panels (including independent ones) and updates the location panel of the global navigation's panel group (leaving the location panel's button highlighted upon navgiation).
    /// </summary>
    /// <param name="panelId">ID of panel to be made location panel of global navigation panel group.</param>
    /// <param name="triggersOnPanelMgmtUpdated">Default "true", causes components that consume _panel to update. Component must subscribe to the event to receive update commands from _panel.</param>
    public void UpdatePanelsWhenNavigating(int panelId)
    {
        PanelService.DeactivateAllPanels();
        PanelService.ActivateLocationButtonsOfGroups();
        PanelService.UpdateGroupLocationPanel(panelId);
        RaiseEventOnUiServiceChanged();
    }
    public void DeactivateCooperativePanels()
    {
        PanelService.DeactivateCooperativePanels();
        PanelService.ActivateLocationButtonsOfGroups();
        RaiseEventOnUiServiceChanged();
    }
    public void ActivatePanel(int selectedPanelId)
    {
        PanelService.ActivatePanel(selectedPanelId);
        RaiseEventOnUiServiceChanged();
    }
    public void DeactivatePanel(int selectedPanelId)
    {
        PanelService.DeactivatePanel(selectedPanelId);
        RaiseEventOnUiServiceChanged();
    }
}
