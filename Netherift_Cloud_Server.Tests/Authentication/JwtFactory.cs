using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Netherift_Cloud_Server.Tests.Authentication
{
    public class JwtFactory
    {
        private const string KEY = ";fhgw[tuh324[thewpruo34th[vwer[otih ewr[tio weht";

        public static Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: "Sample",
                audience: "Sample",
                claims: new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, "meziantou")
                },
                expires: DateTime.UtcNow.AddMinutes(1));

            var handler = new JwtSecurityTokenHandler();
            return Task.FromResult(handler.WriteToken(secToken));
        }
    }
}
