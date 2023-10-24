namespace ProfessionalWebsite.Client.Services.UI;

public class AnimMgmt : IAnimMgmt
{
    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";
    public static AnimMgmt Create() => new();
    public string ToggleAnimation(int animationIndex, IPanelMgmt panelMgmt, string animateMain, List<bool> isContinuous, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panelMgmt, panels, panelGroupsList);
            return string.Empty;
        }
        else if (isContinuous[animationIndex])
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panelMgmt, panels, panelGroupsList);
            return $"main{animationIndex + 1}-infinite";
        }
        else
        {
            SetDiscontinueButton(string.Empty, panelMgmt, panels, panelGroupsList);
            return $"main{animationIndex + 1}";
        }
    }
    public string ToggleContinuousAnimation(int animationIndex, IPanelMgmt panelMgmt, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panelMgmt, panels, panelGroupsList);
            return string.Empty;
        }
        else
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panelMgmt, panels, panelGroupsList);
            return $"main{animationIndex + 1}-infinite";
        }
    }
    public string ToggleOnePlayAnimation(int animationIndex, IPanelMgmt panelMgmt, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panelMgmt, panels, panelGroupsList);
            return string.Empty;
        }
        else
        {
            SetDiscontinueButton(string.Empty, panelMgmt, panels, panelGroupsList);
            return $"main{animationIndex + 1}";
        }
    }
    public string DiscontinueAnimation(IPanelMgmt panelMgmt, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        SetDiscontinueButton(string.Empty, panelMgmt, panels, panelGroupsList);
        return string.Empty;
    }
    private void SetDiscontinueButton(string discontinue, IPanelMgmt panelMgmt, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList)
    {
        if (discontinue == string.Empty)
            panelMgmt.DeactivatePanel(8, panels);
        else
            panelMgmt.ActivatePanel(8, panels, panelGroupsList);
    }
}
