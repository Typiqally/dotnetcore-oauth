using System.Net.Http;
using System.Threading.Tasks;
using NETCore.OAuth.Client.Extensions;
using NETCore.OAuth.Core;

namespace NETCore.OAuth.Client
{
	public class OAuthClient
	{
		private readonly DiscoveryDocument _discoveryDocument;
		private readonly ClientCredentials _credentials;
		private readonly HttpClient _httpClient;

		public OAuthClient(DiscoveryDocument discoveryDocument, ClientCredentials credentials)
		{
			_discoveryDocument = discoveryDocument;
			_credentials = credentials;

			_httpClient = new HttpClient();
		}

		public async Task<TokenResponse> RefreshTokenAsync(TokenResponse token)
		{
			var converter = new GrantTypeConverter();

			var parameters = _credentials.Dictionary();
			parameters.Add("grant_type", converter.ConvertToString(GrantType.RefreshToken));
			parameters.Add("refresh_token", token.RefreshToken);

			var requestMessage = new HttpRequestMessage(HttpMethod.Post, _discoveryDocument.TokenEndpoint)
			{
				Content = new FormUrlEncodedContent(parameters)
			};

			var response = await _httpClient.SendAsync<TokenResponse>(requestMessage);
			return response.Data;
		}
	}
}