using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Presentation;
using Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = (!string.IsNullOrEmpty(builder.Configuration["BaseAddress"])) ? builder.Configuration["BaseAddress"] : builder.HostEnvironment.BaseAddress;

builder.Services.AddTransient(scope => new HttpClient { BaseAddress = new Uri(baseAddress) })
                .AddSingleton<IFunction, Function>()
                .AddMudServices();

await builder.Build().RunAsync();
