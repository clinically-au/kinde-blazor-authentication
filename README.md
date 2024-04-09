# Integrating Kinde Auth with ASP.NET Core 8 Blazor Apps

This is an example of how to use ```Clinically.Kinde.Authentication``` to simplify integrating Kinde with Blazor apps.

See the [repo](https://github.com/clinically-au/kinde-authentication) for that library for more information on how to use it.

The following needs to be in your ```appSettings.json``` on the server:

```json
{
  "Kinde": {
    "Authority": "<From Kinde>",
    "ClientId": "<From Kinde>",
    "ClientSecret": "<From Kinde>",
    "ManagementApiClientId": "<From Kinde>",
    "ManagementApiClientSecret": "<From Kinde>",
    "SignedOutRedirectUri": "https://localhost:5001/signout-callback-oidc",
  },
  "AppConfig": {
    "BaseUrl": "https://localhost:5001"
  }
}
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

## Example Projects

- [JWT Bearer Authentication with Web API and React Client](https://github.com/clinically-au/KindeJwtExample)
- [Blazor App](https://github.com/clinically-au/BlazorAppWithKindeAuthentication)

## To Do List:

- Feature flags not currently implemented (but will work the same way as Permissions)
- Support more claims/properties in the strongly typed user objects