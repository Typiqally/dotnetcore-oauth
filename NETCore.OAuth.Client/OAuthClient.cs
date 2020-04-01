using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using NETCore.OAuth.Client.Extensions;
using NETCore.OAuth.Core;
using NETCore.OAuth.Core.Extensions;

namespace NETCore.OAuth.Client
{
    public class OAuthClient
    {
        private static readonly GrantTypeConverter GrantTypeConverter = new GrantTypeConverter();

        private readonly DiscoveryDocument _discoveryDocument;
        private readonly ClientCredentials _credentials;
        private readonly string _tokenSavePath;
        private readonly HttpClient _httpClient;

        public OAuthClient(DiscoveryDocument discoveryDocument, ClientCredentials credentials, string tokenSavePath = "token.json")
        {
            _discoveryDocument = discoveryDocument;
            _credentials = credentials;
            _tokenSavePath = tokenSavePath;
            _httpClient = new HttpClient();
        }

        public async Task<TokenResponse> StartAuthorizeAsync(Dictionary<string, string> parameters = null, int port = 80)
        {
            if (TryReadTokenFromFile(_tokenSavePath, out var token))
            {
                return token;
            }

            var requestParameters = _credentials.Dictionary();
            requestParameters.Add("response_type", "code");
            requestParameters.AddRange(parameters);

            var uri = _discoveryDocument.AuthorizeEndpoint.AppendQueryParameters(requestParameters);
            BrowserHelper.OpenUrl(uri);

            var listener = new HttpCallbackListener(port);
            string code = null;

            await listener.ListenAsync(message =>
            {
                var query = message.RequestUri.ToString().Split("?")[1];
                if (query.Length == 0) throw new ArgumentOutOfRangeException(nameof(query), "The request uri does not contain a query.");

                code = HttpUtility.ParseQueryString(query)["code"];

                listener.Stop();
            });

            return await RequestTokenWithCodeAsync(code) ?? throw new ArgumentNullException(nameof(code));
        }

        public async Task<TokenResponse> RequestTokenWithCodeAsync(string code)
        {
            var parameters = new Dictionary<string, string>
                {{"grant_type", GrantTypeConverter.ConvertToString(GrantType.AuthorizationCode)}, {"code", code}};

            return await RequestTokenAsync(parameters);
        }

        public async Task<TokenResponse> RefreshTokenAsync(TokenResponse token)
        {
            var parameters = new Dictionary<string, string>
                {{"grant_type", GrantTypeConverter.ConvertToString(GrantType.RefreshToken)}, {"refresh_token", token.RefreshToken}};

            return await RequestTokenAsync(parameters);
        }

        public async Task<TokenResponse> RequestTokenAsync(Dictionary<string, string> parameters)
        {
            var requestParameters = _credentials.Dictionary();
            requestParameters.AddRange(parameters);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _discoveryDocument.TokenEndpoint)
            {
                Content = new FormUrlEncodedContent(requestParameters)
            };

            var response = await _httpClient.SendAsync<TokenResponse>(requestMessage);
            var json = await response.Message.Content.ReadAsStringAsync();

            await File.WriteAllTextAsync(_tokenSavePath, json);
            return response.Data;
        }

        public static bool TryReadTokenFromFile(string file, out TokenResponse token)
        {
            token = null;

            if (!File.Exists(file))
            {
                return false;
            }

            try
            {
                var json = File.ReadAllText(file);
                token = JsonSerializer.Deserialize<TokenResponse>(json);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}