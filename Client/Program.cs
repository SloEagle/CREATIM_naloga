global using CREATIM_naloga.Shared;
global using CREATIM_naloga.Shared.Models;
global using CREATIM_naloga.Client.Services.GroupService;
global using CREATIM_naloga.Client.Services.UserService;
global using CREATIM_naloga.Client.Services.SmsService;

using CREATIM_naloga.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ISmsService, SmsService>();

await builder.Build().RunAsync();
