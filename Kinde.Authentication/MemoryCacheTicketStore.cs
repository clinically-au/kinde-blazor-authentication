using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;

namespace KindeAuthentication;

public class MemoryCacheTicketStore : ITicketStore
{
    private const string AppCachePrefix = "AuthTicket_";
    private readonly MemoryCache _store = new(new MemoryCacheOptions());

    public Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = Guid.NewGuid().ToString();
        _store.Set(AppCachePrefix + key, ticket, TimeSpan.FromMinutes(30));
        return Task.FromResult(key);
    }

    public Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentNullException.ThrowIfNull(ticket);
        _store.Set(AppCachePrefix + key, ticket, TimeSpan.FromMinutes(30));
        return Task.CompletedTask;
    }

    public Task<AuthenticationTicket?> RetrieveAsync(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        return Task.FromResult(_store.TryGetValue(AppCachePrefix + key, out AuthenticationTicket? ticket)
            ? ticket
            : null);
    }

    public Task RemoveAsync(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        _store.Remove(AppCachePrefix + key);
        return Task.CompletedTask;
    }
}