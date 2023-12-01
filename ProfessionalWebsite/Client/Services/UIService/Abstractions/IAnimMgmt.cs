namespace ProfessionalWebsite.Client.Services.UI;

public interface IAnimMgmt
{
    string ToggleAnimation(int animationIndex, string animateMain, List<bool> isContinuous, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
    string ToggleOnePlayAnimation(int animationIndex, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
    string ToggleContinuousAnimation(int animationIndex, string animateMain, Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
    string DiscontinueAnimation(Dictionary<int, Panel> panels, List<PanelGroup> panelGroupsList);
}
