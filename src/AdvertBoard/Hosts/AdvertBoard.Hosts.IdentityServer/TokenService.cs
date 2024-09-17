using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdvertBoard.Hosts.IdentityServer
{
    public class TokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetApiTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (disco.IsError)  
            {
                throw new Exception(disco.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }

        public async Task<string> CallApiAsync(string token)
        {
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(token);

            var response = await apiClient.GetAsync("https://localhost:6001/identity");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API call failed: {response.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}