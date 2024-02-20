namespace ProfessionalWebsite.Client.Services.UI;

public interface IAnimMgmt
{
    string AnimateMain { get; }
    List<bool> IsContinuous { get; }
    //string DiscontinueAnimation { get; }
    void PlayAnimation(int animationIndex, IPanelMgmt panelMgmt);
    void PlayAnimation(int animationIndex, bool isContinuous, IPanelMgmt panelMgmt);
    void DiscontinueAnimation(IPanelMgmt panelMgmt);
}