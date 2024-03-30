using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KindeAuthentication;

public class KindeManagementApiAuthenticationHelper(IConfiguration config, ILogger<KindeManagementClient> logger, IHttpClientFactory httpClientFactory, 
    KindeAuthenticationOptions options)
{
    private string? _authToken;
    private DateTime? _authTokenExpiry = DateTime.MaxValue;
    
    public async Task<string> GetAuthTokenAsync()
    {
        if (DateTime.Now < _authTokenExpiry?.Subtract(TimeSpan.FromMinutes(5)) && !string.IsNullOrEmpty(_authToken)) return _authToken;

        using var client = httpClientFactory.CreateClient();
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", options.ManagementApiClientId),
            new KeyValuePair<string, string>("client_secret", options.ManagementApiClientSecret),
            new KeyValuePair<string, string>("audience", $"{options.Authority}/api"),
        });

        content.Headers.Clear();
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

        var response = await client.PostAsync($"{options.Authority}/oauth2/token", content).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogCritical(@"Unable to authenticate with Kinde Management API {StatusCode} {Error}",
                response.StatusCode, response.ReasonPhrase);
            throw new ApplicationException(
                $"Unable to authenticate with Kinde Management API {response.StatusCode} {response.ReasonPhrase}");
        }

        var json = response.Content.ReadAsStringAsync().Result;
        var jsonDocument = JsonDocument.Parse(json);
        _authToken = jsonDocument.RootElement.GetProperty("access_token").GetString();
        var expClaim = jsonDocument.RootElement.GetProperty("expires_in").GetInt64();
        _authTokenExpiry = DateTimeOffset.FromUnixTimeSeconds(expClaim).DateTime;
        
        if (!string.IsNullOrEmpty(_authToken)) return _authToken;
        
        logger.LogCritical("Kinde Management API did not return an access token");
        throw new ApplicationException("Kinde Management API did not return an access token");
    }

}