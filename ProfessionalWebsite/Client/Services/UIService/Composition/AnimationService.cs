namespace ProfessionalWebsite.Client.Services.UI;

public class AnimationService
{
    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";
    private const int DISCONTINUE_BTN_PANEL_ID = 8;
    private AnimationService()
    {
        AnimateAppContainer = string.Empty;
        IsContinuous = AnimationsTable.Get();
    }
    public static AnimationService Create()
    {
        return new AnimationService();
    }
    public string AnimateAppContainer { get; private set; }
    public List<bool> IsContinuous { get; private set; }

    public void ToggleOneTimeAnimation(
        int animationIndex, 
        PanelService panelsService)
    {
        List<PanelGroup> panelGroupsList = panelsService.PanelGroups.Values.ToList();

        if (AnimateAppContainer == $"main{animationIndex}-infinite" || AnimateAppContainer == $"main{animationIndex}")
        {
            SetDiscontinueButton(string.Empty, panelsService, panelGroupsList);
            AnimateAppContainer = string.Empty;
        }
        else if (IsContinuous[animationIndex])
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panelsService, panelGroupsList);
            AnimateAppContainer = $"main{animationIndex}-infinite";
        }
        else
        {
            SetDiscontinueButton(string.Empty, panelsService, panelGroupsList);
            AnimateAppContainer = $"main{animationIndex}";
        }
    }
    public void ToggleContinuousAnimation(
        int animationIndex, 
        PanelService panelService)
    {
        List<PanelGroup> panelGroupsList = panelService.PanelGroups.Values.ToList();

        if (AnimateAppContainer == string.Empty)  // currently no animation
        {
            // Make it animated
            AnimateAppContainer = $"main{animationIndex}-infinite";
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panelService, panelGroupsList);
        }
        else  // currently animated (maybe infinite/continuous)
        {
            // End the animation
            AnimateAppContainer = string.Empty;
            SetDiscontinueButton(string.Empty, panelService, panelGroupsList);
        }
    }
    public void ToggleOnePlayAnimation(
        int animationIndex, 
        PanelService panelService)
    {
        List<PanelGroup> panelGroupsList = panelService.PanelGroups.Values.ToList();

        if (AnimateAppContainer == $"main{animationIndex}-infinite" || AnimateAppContainer == $"main{animationIndex}")
        {
            SetDiscontinueButton(string.Empty, panelService, panelGroupsList);
            AnimateAppContainer = string.Empty;
        }
        else
        {
            SetDiscontinueButton(string.Empty, panelService, panelGroupsList);
            AnimateAppContainer = $"main{animationIndex}";
        }
    }
    public void DiscontinueAnimation(PanelService panelService)
    {
        List<PanelGroup> panelGroupsList = panelService.PanelGroups.Values.ToList();

        SetDiscontinueButton(string.Empty, panelService, panelGroupsList);
        AnimateAppContainer = string.Empty;
    }
    private void SetDiscontinueButton(
        string discontinue, 
        PanelService panelService, 
        List<PanelGroup> panelGroupsList)
    {
        if (discontinue == string.Empty)
            panelService.DeactivatePanel(DISCONTINUE_BTN_PANEL_ID);
        else
            panelService.ActivatePanel(DISCONTINUE_BTN_PANEL_ID);
    }
}
