using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ProfessionalWebsite.Client.Pages;

public partial class SelectDemo : ComponentBase
{
    private bool testHistoryIsExpanded = false;
    private string viewCodeUrl = string.Empty;
    private string currentPath => Navigation.ToBaseRelativePath(Navigation.Uri).ToLower();
    private string testRedneringProcess = "not yet change";

    protected override void OnInitialized()
    {
        //if (SectionId < 0)
        //{
        //    SectionId = UI.StartingSectionId;
        //}
        //UI.NavigateToSection(SectionId);
        //UI.OnUiServiceChanged += UpdateThisComponent;

    //
        if (SectionId > 0)
        {
            UI.SetPromoSection(SectionId);
        }
    }
    
    //
    [Parameter]
    public int SectionId { get; set; } = -1;

    //void IDisposable.Dispose() =>
    //    UI.OnUiServiceChanged -= UpdateThisComponent;

    private void UpdateThisComponent(string meaninglessRequirement) =>
        StateHasChanged();

    private void ActivateDisclaimerPanel(string projectCodeUrl)
    {
        UI.ActivatePanel(9);  // Activate the appropriate panel
        viewCodeUrl = projectCodeUrl;  // URL of unique project to be used upon next navigation
    }

    private async void ContinueToViewCode()
    {
        await OpenViewCodeUrlInNewTab();
        UI.DeactivatePanel(9);
        viewCodeUrl = string.Empty;
    }
    private async Task OpenViewCodeUrlInNewTab() =>  // Tested: Successful.
        await JSRuntime.InvokeVoidAsync("open", $"https://{viewCodeUrl}", "_blank");

    private async Task Alert() =>  // tested: Successful.
        await JSRuntime.InvokeVoidAsync("alert", "This is an alert! Supp");

    private void ToggleTestHistoryIsExpanded() =>
        testHistoryIsExpanded = !testHistoryIsExpanded;
}
