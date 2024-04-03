using BlazorAppWithKindeAuthentication;
using BlazorAppWithKindeAuthentication.Components;
using BlazorAppWithKindeAuthentication.Components.Account;
using Clinically.Kinde.Authentication;
using Clinically.Kinde.Authentication.Identity;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

// Add the Kinde Authentication services that use ASP.NET Identity
builder.Services.AddKindeAuthentication(builder.Configuration, opt =>
{
    opt.UseJwtBearerValidation = false;
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