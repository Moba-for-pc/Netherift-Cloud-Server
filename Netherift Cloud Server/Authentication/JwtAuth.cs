using Netherift_Cloud_Server.Authentication.JwtEndpoint;
using Netherift_Cloud_Server.Authentication.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
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
            _currentToken = await _endpoint.MakeRequestAsync();
            return _currentToken;  
        }
    }
}
