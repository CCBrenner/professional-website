using ProfessionalWebsite.Client.Classes.Mgmt;

namespace ProfessionalWebsite.Client.Services.Contracts
{
    public interface IUIService
    {
        NavMgmt NavMgmt { get; }
        PanelMgmt PanelMgmt { get; }
        SectionMgmt SectionMgmt { get; }
        AnimMgmt AnimMgmt { get; }
    }
}
