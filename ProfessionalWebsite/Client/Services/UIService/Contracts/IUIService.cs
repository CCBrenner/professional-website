using ProfessionalWebsite.Client.Services.UI.Mgmt;

namespace ProfessionalWebsite.Client.Services.UI.Contracts;

public interface IUIService
{
    NavMgmt Nav { get; }
    PanelMgmt Panel { get; }
    SectionMgmt Section { get; }
    AnimMgmt Anim { get; }
}
