# Integrating Kinde Auth with .NET8 Blazor Server Apps
This is a quick sample of integrating Kinde Authentication with Blazor apps using AspNet core identity

Either fork the repo and use it, or add the following NuGet package:
```WojtJ.Kinde.IdentityAuthentication```

It needs lots of improvements, but is a reasonable starting point.

The following needs to be in your ```appSettings.json```:
```json
  "Kinde": {
    "Authority": "<From Kinde>",
    "ClientId": "<From Kinde>",
    "ClientSecret": "<From Kinde>",
    "ManagementApiClientId": "<From Kinde>",
    "ManagementApiClientSecret": "<From Kinde>",
    "SignedOutRedirectUri": "https://localhost:5001/signout-callback-oidc"
  },
  "AppConfig": {
    "BaseUrl": "https://localhost:5001"
  }
```

You then need the following in your Program.cs (I will soon add something so you can just add the section):
```csharp 
builder.Services.AddKindeAuthentication(opt =>
{
    opt.UseJwkTokenValidation = true; // default to true
    opt.UseMemoryCacheTicketStore = false; // default to false
}); 
```
## Roles
You can use the standard Authorize attribute:
```csharp
[Authorize(Roles = "Admin")]
```

## Permissions
In order to add authorization policies for your Kinde permissions:
```csharp
builder.Services
    .AddAuthorizationBuilder()
    .AddKindePermissionPolicies<Permissions>();
``` 
Then create a Permissions class that contains all the Kinde permissions you want to use:
```csharp
public class Permissions
{
    public const string MyPermissionName = "myPermissionNameInKinde";
}
```
Then you can use the permissions in your controllers or Razor pages:
```csharp
[Authorize(Policy = Permissions.MyPermissionName)]
```

## Notes
- You need to go to the Tokens section of your app, and enable the Roles and Email claims in the access token.
- In order to access the management API (e.g. to add users programmatially etc), inject ```KindeManagementClient``` into your services. Note you will need a separate M2M app in Kinde for this, with access to the Management API.
- You can also inject ```KindeUserManager``` instead of the standard ```UserManager``` to get access to Kinde-specific methods.
- Inject ```BlazorServerUserAccessor``` to get access to the current user in your Blazor server-rendered components.

I've only just recently worked out how to tie all this together, so some bits may not be entirely required etc. Take this as a proof of concept at the moment :-)

### To Do List:
- Feature flags not currently implemented (but will work the same way as Permissions)
- Nuget package