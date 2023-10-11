namespace ProfessionalWebsite.Client.Services.UI;

public interface IUIService
{
    NavMgmt Nav { get; }
    PanelMgmt Panel { get; }
    SectionMgmt Section { get; }
    string AnimateMain { get; }
    List<bool> IsContinuous { get; }
}
