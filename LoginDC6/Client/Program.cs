using Blazored.Toast;
using LoginDC6.Client;
using LoginDC6.Client.Auth;
using LoginDC6.Client.Helpers;
using LoginDC6.Client.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredToast();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<JWTAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>
    (provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

builder.Services.AddScoped<ILoginService, JWTAuthenticationStateProvider>
    (provider => provider.GetRequiredService<JWTAuthenticationStateProvider>());

//builder.Services.AddBaseAddressHttpClient();
builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();