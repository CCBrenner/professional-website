using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProfessionalWebsite.Client;
using ProfessionalWebsite.Client.Services;
using ProfessionalWebsite.Client.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<UIService>();
builder.Services.AddScoped<ICounterService, CounterService>();

await builder.Build().RunAsync();
