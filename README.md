This is a quick and dirty sample of integrating Kinde Authentication with Blazor apps using AspNet core identity

It needs lots of improvements, but is a reasonable starting point.

The following needs to be in your ```appSettings.json```:
```
  "Kinde": {
    "Authority": "<From Kinde>",
    "ClientId": "<From Kinde>",
    "ClientSecret": "<From Kinde>",
    "SignedOutRedirectUri": "https://localhost:5001/signout-callback-oidc"
  },
  "AppConfig": {
    "BaseUrl": "https://localhost:5001"
  }
```

You also need to go to the Tokens section of your app, and enable the Roles and Email claims in the access token.

Some key points for improvement from here:
- I've integrated it with AspNet Core Identity mostly because I am using some other libraries that rely on AspNet Core Identity being there.
- If you're happy to store all custom user claims in Kinde rather than in a local Db, then you can dispense entirely with AspNet Core Identity, but then will need to implement a fair bit more e.g. authorization attributes etc yourself. 
- I've only just recently worked out how to tie all this together, so some bits may not be entirely required etc. Take this as a proof of concept at the moment :-)

Other things to consider:
- Need to build in a way to access the Management API (either using the official Kinde SDK or your own generated from the OpenApi spec)
- Need to build a UI for the Management API
