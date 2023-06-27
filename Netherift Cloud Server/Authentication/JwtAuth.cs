using Netherift_Cloud_Server.Authentication.JwtEndpoint;
using Netherift_Cloud_Server.Authentication.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.IdentityModel.Tokens.Jwt;
using Unity.Services.CloudCode.Core;

namespace Netherift_Cloud_Server.Authentication
{
    public class JwtAuth : IJwtAuth
    {
        private IJwtEndpoint _endpoint;

        private string _currentToken;
        public JwtAuth(IJwtEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<string> GetTokenAsync()
        {
            if(_currentToken == null || IsExpiredSoon(_currentToken))
            {
                _currentToken = await GetNewTokenAsync();
            }
            return _currentToken;
        }

        private async Task<string> GetNewTokenAsync()
        {
            var token = await _endpoint.MakeRequestAsync();
            return token;
        }

        private bool IsExpiredSoon(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            return jsonToken.ValidTo < DateTime.UtcNow || 
                jsonToken.ValidTo < DateTime.UtcNow.AddSeconds(30);
        }
    }
}
