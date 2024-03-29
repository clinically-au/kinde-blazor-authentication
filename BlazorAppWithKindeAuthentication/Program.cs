using BlazorAppWithKindeAuthentication;
using BlazorAppWithKindeAuthentication.Components;
using BlazorAppWithKindeAuthentication.Components.Account;
using KindeAuthentication;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

// Add the Kinde Authentication services that use ASP.NET Identity
builder.Services.AddKindeAuthentication(opt =>
{
    opt.Authority = builder.Configuration["Kinde:Authority"] ??
                    throw new ArgumentNullException($"Kinde:Authority",
                        "Kinde:Authority is required in the app settings");
    opt.ClientId = builder.Configuration["Kinde:ClientId"] ??
                   throw new ArgumentNullException($"Kinde:ClientId", "Kinde:ClientId is required in the app settings");
    opt.ClientSecret = builder.Configuration["Kinde:ClientSecret"] ?? throw new ArgumentNullException(
        $"Kinde:ClientSecret",
        "Kinde:ClientSecret is required in the app settings");
    opt.SignedOutRedirectUri = builder.Configuration["Kinde:SignedOutRedirectUri"] ?? throw new ArgumentNullException(
        $"Kinde:SignedOutRedirectUri", "Kinde:SignedOutRedirectUri is required in the app settings");
    opt.ManagementApiClientId = builder.Configuration["Kinde:ManagementApiClientId"] ?? throw new ArgumentNullException(
        $"Kinde:ManagementApiClientId", "Kinde:ManagementApiClientId is required in the app settings");
    opt.ManagementApiClientSecret = builder.Configuration["Kinde:ManagementApiClientSecret"] ??
                                    throw new ArgumentNullException($"Kinde:ManagementApiClientSecret",
                                        "Kinde:ManagementApiClientSecret is required in the app settings");
    opt.UseJwkTokenValidation = true;
    opt.UseMemoryCacheTicketStore = false;
});

builder.Services
    .AddAuthorizationBuilder()
    .AddKindePermissionPolicies<Permissions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

// Add the Kinde Identity endpoints
app.MapKindeIdentityEndpoints();

app.Run();