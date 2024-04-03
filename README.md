# Integrating Kinde Auth with .NET8 Blazor Apps

This example shows how to integrate Kinde with .NET applications, using my implementation that integrates Kinde with ASP.NET Identity. The library is still in development, but usable.

Add the following NuGet package:
```Clinically.Kinde.Authentication```

The source code for this library is at https://github.com/clinically-au/kinde-authentication

The following needs to be in your ```appSettings.json``` on the server:

```json
"Kinde": {
  "Authority": "<From Kinde>",
  "ClientId": "<From Kinde>",
  "ClientSecret": "<From Kinde>",
  "ManagementApiClientId": "<From Kinde>",
  "ManagementApiClientSecret": "<From Kinde>",
  "SignedOutRedirectUri": "https://localhost:5001/signout-callback-oidc",
  "JwtAudience": "<From Kinde - Audience for API, if using JWT Bearer Auth in addition to Identity>",
},
"AppConfig": {
  "BaseUrl": "https://localhost:5001"
}
```

You then need the following in your server-side ```Program.cs```:

```csharp 
builder.Services.AddKindeAuthentication(opt =>
{
    opt.UseJwtBearerValidation = false; // default to false
    opt.UseMemoryCacheTicketStore = false; // default to false
}); 
```

For Blazor WASM, you also need to add this to ```Program.cs``` on the client:

```csharp
builder.Services.AddKindeWebAssemblyAuthentication();
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
- In order to access the management API (e.g. to add users programmatially etc), inject ```KindeManagementClient``` into
  your services. Note you will need a separate M2M app in Kinde for this, with access to the Management API.
- You can also inject ```KindeUserManager``` instead of the standard ```UserManager``` to get access to Kinde-specific
  methods.
- Inject ```BlazorUserAccessor``` to get access to the current user in your Blazor components.

I've only just recently worked out how to tie all this together, so some bits may not be entirely required etc. Take
this as a proof of concept at the moment :-)

### To Do List:

- Feature flags not currently implemented (but will work the same way as Permissions)
- Support more claims/properties in the strongly typed user objects