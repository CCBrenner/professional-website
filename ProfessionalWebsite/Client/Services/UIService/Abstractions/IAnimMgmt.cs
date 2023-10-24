namespace ProfessionalWebsite.Client.Services.UI;

public interface IAnimMgmt
{
    //string AnimateMain { get; }
    string ToggleAnimation(int animationIndex, PanelMgmt panelMgmt, string animateMain, List<bool> isContinuous);
    string ToggleOnePlayAnimation(int animationIndex, PanelMgmt panelMgmt, string animateMain);
    string ToggleContinuousAnimation(int animationIndex, PanelMgmt panelMgmt, string animateMain);
    string DiscontinueAnimation(PanelMgmt panelMgmt);
}
