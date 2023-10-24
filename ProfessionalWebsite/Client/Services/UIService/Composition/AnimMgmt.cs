namespace ProfessionalWebsite.Client.Services.UI;

public class AnimMgmt : IAnimMgmt
{
    private AnimMgmt()
    {
        //AnimateMain = string.Empty;
        //_isContinuous = animationsIsContinuousInitialization;
    }

    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";

    public static AnimMgmt Create() => new();
    public string ToggleAnimation(int animationIndex, PanelMgmt panelMgmt, string animateMain, List<bool> isContinuous)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panelMgmt);
            return string.Empty;
        }
        else if (isContinuous[animationIndex])
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panelMgmt);
            return $"main{animationIndex + 1}-infinite";
        }
        else
        {
            SetDiscontinueButton(string.Empty, panelMgmt);
            return $"main{animationIndex + 1}";
        }
    }
    public string ToggleContinuousAnimation(int animationIndex, PanelMgmt panelMgmt, string animateMain)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panelMgmt);
            return string.Empty;
        }
        else
        {
            SetDiscontinueButton(DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panelMgmt);
            return $"main{animationIndex + 1}-infinite";
        }
    }
    public string ToggleOnePlayAnimation(int animationIndex, PanelMgmt panelMgmt, string animateMain)
    {
        if (animateMain == $"main{animationIndex + 1}-infinite" || animateMain == $"main{animationIndex + 1}")
        {
            SetDiscontinueButton(string.Empty, panelMgmt);
            return string.Empty;
        }
        else
        {
            SetDiscontinueButton(string.Empty, panelMgmt);
            return $"main{animationIndex + 1}";
        }
    }
    public string DiscontinueAnimation(PanelMgmt panelMgmt)
    {
        SetDiscontinueButton(string.Empty, panelMgmt);
        return string.Empty;
    }
    private void SetDiscontinueButton(string discontinue, PanelMgmt panelMgmt)
    {
        if (discontinue == string.Empty)
            panelMgmt.DeactivatePanel(8);
        else
            panelMgmt.ActivatePanel(8);
    }
}
