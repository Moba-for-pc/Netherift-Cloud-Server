using Netherift_Cloud_Server.Authentication.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Unity.Services.CloudCode.Core;

namespace Netherift_Cloud_Server.Authentication.JwtEndpoint
{
    public class JwtEndpoint : IJwtEndpoint
    {
        private const string API_URL = "https://services.api.unity.com";
        private const string TOKEN_EXCHANGE_URN = "auth/v1/token-exchange";
        private const string PROJECT_ID_QUERY_NAME = "projectId";
        private const string ENVIRONMENT_ID_QUERY_NAME = "environmentId";

        private IExecutionContext _context;
        public JwtEndpoint(IExecutionContext context)
        {
            _context = context;
        }

        public async Task<string> MakeRequestAsync()
        {
            var client = await ConfigureRestClientAsync();
            var request = ConfigureRequestToTokenExchange();
            var response = await client.ExecuteAsync(request);
            var token = DeserializeToken(response);
            return token;
        }

        private async Task<RestClient> ConfigureRestClientAsync()
        {
            var options = await ConfigureClientOptions();
            var client = new RestClient(options);
            return client;
        }

        private Task<RestClientOptions> ConfigureClientOptions()
        {
            var clientOptions = new RestClientOptions()
            {
                BaseUrl = new Uri(API_URL),
                Authenticator = new HttpBasicAuthenticator(ServiceAccountCredentials.KEY_ID, ServiceAccountCredentials.SECRET),
            };
            return Task.FromResult(clientOptions);
        }

        private RestRequest ConfigureRequestToTokenExchange()
        {
            var request = new RestRequest(TOKEN_EXCHANGE_URN)
              .AddQueryParameter(PROJECT_ID_QUERY_NAME, _context.ProjectId, false)
              .AddQueryParameter(ENVIRONMENT_ID_QUERY_NAME, _context.EnvironmentId, false);
            request.Method = Method.Post;
            return request;
        }

        private string DeserializeToken(RestResponse response)
        {
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
