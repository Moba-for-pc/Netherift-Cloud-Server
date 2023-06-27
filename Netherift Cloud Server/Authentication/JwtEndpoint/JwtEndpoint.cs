using Netherift_Cloud_Server.Authentication.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.CloudCode.Core;

namespace Netherift_Cloud_Server.Authentication.JwtEndpoint
{
    public class JwtEndpoint : IJwtEndpoint
    {
        private IExecutionContext _context;
        public JwtEndpoint(IExecutionContext context) {
            _context = context;
        }

        public async Task<string> MakeRequestAsync()
        {
            var clientOptions = new RestClientOptions()
            {
                BaseUrl = new Uri("https://services.api.unity.com"),
                Authenticator = new HttpBasicAuthenticator(ServiceAccountCredentials.KEY_ID, ServiceAccountCredentials.SECRET),
            };
            var client = new RestClient(clientOptions);

            var request = new RestRequest("auth/v1/token-exchange")
               .AddQueryParameter("projectId", _context.ProjectId, false)
               .AddQueryParameter("environmentId", _context.EnvironmentId, false);
            request.Method = Method.Post;

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            var json = JsonConvert.DeserializeObject<AuthResponse>(response!.Content);
            var token = json.AccessToken;
            return token;
        }
    }
}
