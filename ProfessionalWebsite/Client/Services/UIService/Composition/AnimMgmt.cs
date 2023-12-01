namespace ProfessionalWebsite.Client.Services.UI;

public class AnimMgmt : IAnimMgmt
{
    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";
    public static AnimMgmt Create() => new();
    public string ToggleAnimation(int animationIndex, string animateMain, List<bool> isContinuous, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return string.Empty;
        }
        else if (isContinuous[animationIndex])
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panels, panelGroupsList);
            return $"main{animationIndex + 1}-infinite";
        }
        else
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return $"main{animationIndex + 1}";
        }
    }
    public string ToggleContinuousAnimation(int animationIndex, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return string.Empty;
        }
        else
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panels, panelGroupsList);
            return $"main{animationIndex + 1}-infinite";
        }
    }
    public string ToggleOnePlayAnimation(int animationIndex, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return string.Empty;
        }
        else
        {
            SetDiscontinueButton(string.Empty, panels, panelGroupsList);
            return $"main{animationIndex + 1}";
        }
    }
    public string DiscontinueAnimation(Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        SetDiscontinueButton(string.Empty, panels, panelGroupsList);
        return string.Empty;
    }
    private void SetDiscontinueButton(string discontinue, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (discontinue == string.Empty)
            PanelMgmt.DeactivatePanel(8, panels);
        else
            PanelMgmt.ActivatePanel(8, panels, panelGroupsList);
    }
}
