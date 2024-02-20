namespace ProfessionalWebsite.Client.Services.UI;

public class AnimMgmt : IAnimMgmt
{
    private AnimMgmt(List<bool> animationsIsContinuousInitialization)
    {
        AnimateMain = "";
        IsContinuous = animationsIsContinuousInitialization;
    }

    private const string DISCONTINUE_BTN_ACTIVE_CLASS_NAME = "discontinue-button-on";

    public string AnimateMain { get; private set; }
    public List<bool> IsContinuous { get; private set; }

    public event Action<string> OnAnimMgmtChanged;
    public static AnimMgmt Create(List<bool> animationsIsContinuousInitialization) =>
        new(animationsIsContinuousInitialization);

    /// <summary>
    /// Adds a class to the main container, causing everything in it to move based on the keyframes animation defined in the CSS of the component containing main.
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    public void PlayAnimation(int animationIndex, IPanelMgmt panelMgmt) =>
        PlayAnimation(animationIndex, IsContinuous[animationIndex], panelMgmt);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animationIndex">Index of the animation to be applied to the main container.</param>
    /// <param name="isContinuous">Determines whether the animation should be played once or looped continuously.</param>
    public void PlayAnimation(int animationIndex, bool isContinuous, IPanelMgmt panelMgmt)
    {
        if (AnimateMain == $"main{animationIndex + 1}-infinite" || AnimateMain == $"main{animationIndex + 1}")
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
    public void DiscontinueAnimation(IPanelMgmt panelMgmt) =>
        SetAnimateMainAndDiscontinueButton("", "", panelMgmt);

    /// <summary>
    /// Sets the appropriate animation and discontinue class values.
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="discontinue"></param>
    private void SetAnimateMainAndDiscontinueButton(string animation, string discontinue, IPanelMgmt panelMgmt)
    {
        AnimateMain = animation;
        if (discontinue == "")
            panelMgmt.DeactivatePanel(8);
        else
            panelMgmt.ActivatePanel(8);
    }

    /// <summary>
    /// Updates the component that consumes it when a method in the AnimMgmt class that consumes this method invokes/signals that a change to the state of it has occurred.
    /// </summary>
    private void RaiseEventOnAnimMgmtChanged()
    {
        OnAnimMgmtChanged?.Invoke("");
    }
}
