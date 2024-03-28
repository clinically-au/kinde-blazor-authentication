using BlazorAppWithKindeAuthentication.Components;
using BlazorAppWithKindeAuthentication.Components.Account;
using BlazorAppWithKindeAuthentication.Data;
using KindeAuthentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// I know I should learn how to do the options the .NET way; maybe later!
builder.Services.AddKindeAuthentication(new KindeAuthenticationOptions
{
    Authority = builder.Configuration["Kinde:Authority"] ??
                throw new ArgumentNullException($"Kinde:Authority", "Kinde:Authority is required in the app settings"),
    ClientId = builder.Configuration["Kinde:ClientId"] ??
               throw new ArgumentNullException($"Kinde:ClientId", "Kinde:ClientId is required in the app settings"),
    ClientSecret = builder.Configuration["Kinde:ClientSecret"] ?? throw new ArgumentNullException($"Kinde:ClientSecret",
        "Kinde:ClientSecret is required in the app settings"),
    SignedOutRedirectUri = builder.Configuration["Kinde:SignedOutRedirectUri"] ?? throw new ArgumentNullException(
        $"Kinde:SignedOutRedirectUri",
        "Kinde:SignedOutRedirectUri is required in the app settings"),
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager();

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

app.MapAdditionalIdentityEndpoints();

app.Run();