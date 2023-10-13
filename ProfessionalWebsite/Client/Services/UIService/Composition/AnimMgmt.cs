namespace ProfessionalWebsite.Client.Services.UI;

public class AnimMgmt
{
    public AnimMgmt(string animateMain, List<bool> animationsIsContinuousInitialization)
    {
        _animateMain = animateMain;
        _isContinuous = animationsIsContinuousInitialization;
    }

    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";

    private List<bool> _isContinuous;
    private string _animateMain;

    /// <summary>
    /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    public void PlayAnimation(int animationIndex, PanelMgmt panelMgmt) =>
        PlayAnimation(animationIndex, _isContinuous[animationIndex], panelMgmt);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    /// <param name="isContinuous">Determines whether the animation should be played once or looped continuously.</param>
    public void PlayAnimation(int animationIndex, bool isContinuous, PanelMgmt panelMgmt)
    {
        if (_animateMain == $"main{animationIndex + 1}-infinite" || _animateMain == $"main{animationIndex + 1}")
            SetAnimateMainAndDiscontinueButton("", "", panelMgmt);
        else
        {
            if (isContinuous)
                SetAnimateMainAndDiscontinueButton($"main{animationIndex + 1}-infinite", DISCONTINUE_BTN_ACTIVE_CLASS_NAME, panelMgmt);
            else
                SetAnimateMainAndDiscontinueButton($"main{animationIndex + 1}", "", panelMgmt);
        }
    }

    /// <summary>
    /// Stops continuous animation by chaning the animation class to blank (""); also hides the Discontinue button by the same means.
    /// </summary>
    public void DiscontinueAnimation(PanelMgmt panelMgmt) =>
        SetAnimateMainAndDiscontinueButton("", "", panelMgmt);

    /// <summary>
    /// Sets the appropriate animation and discontinue class values.
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="discontinue"></param>
    private void SetAnimateMainAndDiscontinueButton(string animation, string discontinue, PanelMgmt panelMgmt)
    {
        _animateMain = animation;
        if (discontinue == "")
            panelMgmt.DeactivatePanel(8);
        else
            panelMgmt.ActivatePanel(8);
    }
}
