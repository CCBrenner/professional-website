namespace ProfessionalWebsite.Client.Services.UI;

public class Animations
{
    public Animations(string initAnimateMain)
    {
        AnimateAppContainer = initAnimateMain;
    }
    public string AnimateAppContainer { get; private set; }

    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";
    private const int DISCONTINUE_BTN_PANEL_ID = 8;
    public void ToggleAnimation(int animationIndex, List<bool> isContinuous, Panels panels, List<PanelGroup> panelGroupsList)
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
    public void ToggleContinuousAnimation(int animationIndex, Panels panels, List<PanelGroup> panelGroupsList)
    {
        if (AnimateAppContainer == $"main{animationIndex + 1}-infinite" || AnimateAppContainer == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            AnimateAppContainer = string.Empty;
        }
        else
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panels, panelGroupsList);
            AnimateAppContainer = $"main{animationIndex + 1}-infinite";
        }
    }
    public void ToggleOnePlayAnimation(int animationIndex, Panels panels, List<PanelGroup> panelGroupsList)
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
    public string DiscontinueAnimation(Panels panels, List<PanelGroup> panelGroupsList)
    {
        SetDiscontinueButton(string.Empty, panels, panelGroupsList);
        return string.Empty;
    }
    private void SetDiscontinueButton(string discontinue, Panels panels, List<PanelGroup> panelGroupsList)
    {
        if (discontinue == string.Empty)
            panels.DeactivatePanel(DISCONTINUE_BTN_PANEL_ID);
        else
            panels.ActivatePanel(DISCONTINUE_BTN_PANEL_ID, panelGroupsList);
    }
}
