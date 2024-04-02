namespace KindeAuthentication;

public class KindeUserInfo
{
    public required string Id { get; set; }
    public required string Email { get; set; }
    public string? DisplayName { get; set; }
    public string? ProfileUrl { get; set; }
    public string[]? Roles { get; set; }
    public string[]? Permissions { get; set; }
}