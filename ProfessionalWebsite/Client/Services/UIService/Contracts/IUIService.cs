using ProfessionalWebsite.Client.Services.UIService.Mgmt;

namespace ProfessionalWebsite.Client.Services.UIService.Contracts
{
    public interface IUIService
    {
        NavMgmt NavMgmt { get; }
        PanelMgmt PanelMgmt { get; }
        SectionMgmt SectionMgmt { get; }
        AnimMgmt AnimMgmt { get; }
    }
}
