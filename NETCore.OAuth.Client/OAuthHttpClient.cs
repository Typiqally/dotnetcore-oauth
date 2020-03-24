using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NETCore.OAuth.Client.Extensions;
using NETCore.OAuth.Core;

namespace NETCore.OAuth.Client
{
    public class OAuthHttpClient
    {
        private readonly OAuthClient _client;
        private readonly HttpClient _httpClient;

        private TokenResponse _token;

        public OAuthHttpClient(OAuthClient client, TokenResponse token)
        {
            _client = client;
            _token = token;

            _httpClient = new HttpClient();
            _httpClient.SetBearerToken(_token.AccessToken);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.Unauthorized)
                return response;
            
            _token = await _client.RefreshTokenAsync(_token);
            _httpClient.SetBearerToken(_token.AccessToken);

            var retryRequest = new HttpRequestMessage(request.Method, request.RequestUri);
            response = await _httpClient.SendAsync(retryRequest);

            return response;
        }
    }
}