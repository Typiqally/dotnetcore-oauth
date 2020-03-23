using System.Net.Http;
using System.Net.Http.Headers;

namespace NETCore.OAuth.Client.Extensions
{
	public static class HttpClientOAuthExtensions
	{
		public static void SetBearerToken(this HttpClient httpClient, string bearerToken)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
		}
	}
}