namespace ProfessionalWebsite.Client.Services.UI;

public interface IAnimMgmt
{
    string ToggleAnimation(int animationIndex, IPanelMgmt panelMgmt, string animateMain, List<bool> isContinuous, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
    string ToggleOnePlayAnimation(int animationIndex, IPanelMgmt panelMgmt, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
    string ToggleContinuousAnimation(int animationIndex, IPanelMgmt panelMgmt, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
    string DiscontinueAnimation(IPanelMgmt panelMgmt, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
}
