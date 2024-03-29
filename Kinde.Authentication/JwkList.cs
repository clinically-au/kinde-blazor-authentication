using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace KindeAuthentication;

public static class JwksHelper
{

    public static IEnumerable<SecurityKey> LoadKeysFromJson(string json)
    {
        var jwks = JsonSerializer.Deserialize<Jwks>(json);

        return jwks.keys.Select(jwk => new JsonWebKey
        {
            Kty = jwk.kty,
            E = jwk.e,
            N = jwk.n,
            Kid = jwk.kid,
            X5t = jwk.x5t,
            Alg = jwk.alg,
            Use = jwk.use
        });
    }

    public class Jwks
    {
        public List<Jwk> keys { get; set; }
    }

    public class Jwk
    {
        public string kty { get; set; }
        public string use { get; set; }
        public string alg { get; set; }
        public string kid { get; set; }
        public string n { get; set; }
        public string e { get; set; }
        public string x5t { get; set; }
    }
}