using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProfessionalWebsite.Client;
using ProfessionalWebsite.Client.Services.CounterService;
using ProfessionalWebsite.Client.Services.SudokuService;
using ProfessionalWebsite.Client.Services.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CounterService>();
builder.Services.AddSingleton(sp => UIService.Create(
    AnimationsTable.GetIsContinuous(),
    PanelGroupsTable.GetPanelGroupsDict(),
    PanelsTable.GetPanelsDict(),
    SectionedPagesTable.GetSectionedPagesDict(),
    SectionsTable.GetSectionsDict()));
builder.Services.AddSingleton<SudokuService>();

await builder.Build().RunAsync();
