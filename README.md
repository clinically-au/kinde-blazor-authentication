This is a quick sample of integrating Kinde Authentication with Blazor apps using AspNet core identity

It needs lots of improvements, but is a reasonable starting point.

The following needs to be in your ```appSettings.json```:
```
  "Kinde": {
    "Authority": "<From Kinde>",
    "ClientId": "<From Kinde>",
    "ClientSecret": "<From Kinde>",
    "ManagementApiClientId: "<From Kinde>",
    "ManagementApiClientSecret": "<From Kinde>",
    "SignedOutRedirectUri": "https://localhost:5001/signout-callback-oidc"
  },
  "AppConfig": {
    "BaseUrl": "https://localhost:5001"
  }
```

You also need to go to the Tokens section of your app, and enable the Roles and Email claims in the access token.

I've only just recently worked out how to tie all this together, so some bits may not be entirely required etc. Take this as a proof of concept at the moment :-)

Other things to consider:
- Need to build a UI for the Management API
