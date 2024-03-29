using System.Security.Claims;
using Kinde.ManagementApi.Model;
using Microsoft.AspNetCore.Identity;

namespace KindeAuthentication;

public class KindeUser : IdentityUser
{
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? ProvidedId { get; set; }
    public bool IsSuspended { get; set; }
    public string? Picture { get; set; }
    public int? TotalSignIns { get; set; }
    public int? FailedSignIns { get; set; }
    public string? LastSignedIn { get; set; }
    public string? CreatedOn { get; set; }
    public List<string>? Organizations { get; set; }
    public List<Dictionary<string,string>>? Identities { get; set; }
    
    public string? OrganizationCode { get; set; } // Only for logged in users
    
    public List<Claim>? Claims { get; set; }

    internal static KindeUser FromUserResponse(UsersResponseUsersInner user)
    {
        return new KindeUser()
        {
            Id = user.Id,
            UserName = user.Email,
            Email = user.Email,
            GivenName = user.FirstName,
            FamilyName = user.LastName,
            ProvidedId = user.ProvidedId,
            IsSuspended = user.IsSuspended,
            Picture = user.Picture,
            TotalSignIns = user.TotalSignIns,
            FailedSignIns = user.FailedSignIns,
            LastSignedIn = user.LastSignedIn,
            CreatedOn = user.CreatedOn,
            Organizations = user.Organizations,
            Identities = user.Identities?.Select(x => new Dictionary<string, string> { { x.Type.ToString(), x.Identity } }).ToList()
        };
    }
}
