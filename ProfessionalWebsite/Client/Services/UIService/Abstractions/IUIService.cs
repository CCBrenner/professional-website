namespace ProfessionalWebsite.Client.Services.UI;

public interface IUIService
{
    PanelMgmt Panel { get; }
    SectionMgmt Section { get; }
    string AnimateMain { get; }
    List<bool> IsContinuous { get; }
}
