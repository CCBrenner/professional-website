namespace ProfessionalWebsite.Client.Services.UI;

public class Animations
{
    private Animations(string initAnimateMain)
    {
        AnimateAppContainer = initAnimateMain;
    }
    public string AnimateAppContainer { get; private set; }

    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";
    private const int DISCONTINUE_BTN_PANEL_ID = 8;
    public static Animations Create(string initAnimateMain) => new Animations(initAnimateMain);
    public void ToggleAnimation(
        int animationIndex, 
        List<bool> isContinuous, 
        Panels panels, 
        List<PanelGroup> panelGroupsList)
    {
        if (AnimateAppContainer == $"main{animationIndex + 1}-infinite" || AnimateAppContainer == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            AnimateAppContainer = string.Empty;
        }
        else if (isContinuous[animationIndex])
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panels, panelGroupsList);
            AnimateAppContainer = $"main{animationIndex + 1}-infinite";
        }
        else
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            AnimateAppContainer = $"main{animationIndex + 1}";
        }
    }
    public void ToggleContinuousAnimation(
        int animationIndex, 
        Panels panels, 
        List<PanelGroup> panelGroupsList)
    {
        if (AnimateAppContainer == string.Empty)  // currently no animation
        {
            // Make it animated
            AnimateAppContainer = $"main{animationIndex + 1}-infinite";
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panels, panelGroupsList);
        }
        else  // currently animated (maybe infinite/continuous)
        {
            // End the animation
            AnimateAppContainer = string.Empty;
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
        }
    }
    public void ToggleOnePlayAnimation(
        int animationIndex, 
        Panels panels, 
        List<PanelGroup> panelGroupsList)
    {
        if (AnimateAppContainer == $"main{animationIndex + 1}-infinite" || AnimateAppContainer == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            AnimateAppContainer = string.Empty;
        }
        else
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            AnimateAppContainer = $"main{animationIndex + 1}";
        }
    }
    public void DiscontinueAnimation(Panels panels, List<PanelGroup> panelGroupsList)
    {
        SetDiscontinueButton(string.Empty, panels, panelGroupsList);
        AnimateAppContainer = string.Empty;
    }
    private void SetDiscontinueButton(
        string discontinue, 
        Panels panels, 
        List<PanelGroup> panelGroupsList)
    {
        if (discontinue == string.Empty)
            panels.DeactivatePanel(DISCONTINUE_BTN_PANEL_ID);
        else
            panels.ActivatePanel(DISCONTINUE_BTN_PANEL_ID, panelGroupsList);
    }
}
