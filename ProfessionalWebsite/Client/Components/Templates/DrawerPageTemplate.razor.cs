using Microsoft.AspNetCore.Components;

namespace ProfessionalWebsite.Client.Components.Templates;

public partial class DrawerPageTemplate
{
    [Parameter]
    public string HeaderIconClasses { get; set; }
    [Parameter]
    public string HeaderText { get; set; }
    [Parameter]
    public RenderFragment? DrawerContent { get; set; }
    [Parameter]
    public RenderFragment? PageContent { get; set; }
    private string DrawerLvlOneStatus = "drawer-lvl-one-visible";
    private string DrawerLvlTwoStatus = "drawer-lvl-two-visible";
    private string ExternalToggleStatus = "external-toggle-hidden";
    private string InternalToggleStatus = "";
    private void ToggleDrawer()
    {
        ExternalToggleStatus = DrawerLvlOneStatus == "" ? "external-toggle-hidden" : "";
        InternalToggleStatus = DrawerLvlOneStatus == "" ? "" : "internal-toggle-hidden";
        DrawerLvlTwoStatus = DrawerLvlOneStatus == "" ? "drawer-lvl-two-visible" : "";
        DrawerLvlOneStatus = DrawerLvlOneStatus == "" ? "drawer-lvl-one-visible" : "";
    }
}