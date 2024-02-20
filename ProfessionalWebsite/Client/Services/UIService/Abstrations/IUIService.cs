namespace ProfessionalWebsite.Client.Services.UI;

public interface IUIService
{
    string AnimateMain { get; }
    event Action<string> OnUiServiceChanged;
    void ToggleSection(int sectionId);
    void ToggleAllSections(int pageId);
    Panel Panel(int panelId);
    void ActivatePanel(int selectedPanelId);
    Section Section(int sectionId);
    SectionedPage SectionedPage(int sectionId);
    void DeactivateAllPanels(
        bool setActivePanelGroupToLocationPanel,
        bool triggersOnPanelMgmtUpdated = true,
        bool includeIndependentPanels = false
    );
    void DeactivatePanel(int selectedPanelId);
    void TogglePanel(int selectedPanelId);
    void UpdateGroupLocationPanel(int panelId);
    void UpdatePanelsWhenNavigating(int panelId);
    List<bool> IsContinuous { get; }
    void PlayAnimation(int animationIndex);
    void PlayAnimation(int animationIndex, bool isContinuous);
    void DiscontinueAnimation();
    void NavigateToSection(Sec sectionId, bool triggersOnPanelMgmtUpdated = true);
    void NavigateToHardCodedPage(int hardcodedPanelId, int navGroupPanelId);
}
