using Kinde.ManagementApi.Api;
using Kinde.ManagementApi.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindeAuthentication;

public class KindeManagementClient(
    ILogger<KindeManagementClient> logger,
    IHttpClientFactory httpClientFactory,
    KindeManagementApiAuthenticationHelper authenticationHelper,
    IOptions<KindeAuthenticationOptions> options
    )
    : IDisposable
{
    private readonly string? _authority = options.Value.Authority;
    private Configuration? _clientConfiguration;
    private string? _authToken;
    private HttpClient? _client;
    private HttpClientHandler? _clientHandler;

    private async Task<Configuration> GetConfiguration()
    {
        if (_clientConfiguration is not null) return _clientConfiguration;
        ArgumentException.ThrowIfNullOrEmpty(_authority, nameof(_authority));
        
        if (string.IsNullOrEmpty(_authToken)) _authToken = await authenticationHelper.GetAuthTokenAsync().ConfigureAwait(false);
        ArgumentException.ThrowIfNullOrEmpty(_authToken, nameof(_authToken));
        
        _clientConfiguration = new Configuration
        {
            BasePath = _authority,
            AccessToken = _authToken
        };
        
        return _clientConfiguration;
    }

    private HttpClient GetClient()
    {
        if (_client is not null) return _client;

        _client = httpClientFactory.CreateClient();

        return _client;
    }
    
    private HttpClientHandler GetClientHandler()
    {
        if (_clientHandler is not null) return _clientHandler;

        _clientHandler = new HttpClientHandler();
        return _clientHandler;
    }

    public async Task<OrganizationsApi> GetOrganizationClient() => 
        new(GetClient(), await GetConfiguration().ConfigureAwait(false), GetClientHandler());
    public async Task<UsersApi> GetUserClient() => 
        new(GetClient(), await GetConfiguration().ConfigureAwait(false), GetClientHandler());
    public async Task<RolesApi> GetRolesClient() => 
        new(GetClient(), await GetConfiguration().ConfigureAwait(false), GetClientHandler());  
    public async Task<PermissionsApi> GetPermissionsClient() => 
        new(GetClient(), await GetConfiguration().ConfigureAwait(false), GetClientHandler());
    
    public void Dispose()
    {
        _client?.Dispose();
        _clientHandler?.Dispose();
    }
}

