using System.Net.Http;
using System.Net.Http.Headers;

namespace NETCore.OAuth.Client.Extensions
{
	public static class BearerTokenExtensions
	{
		public static void AddBearerToken(this HttpRequestHeaders headers, string bearerToken)
		{
			headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
		}

		public static void SetBearerToken(this HttpClient httpClient, string bearerToken)
		{
			httpClient.DefaultRequestHeaders.AddBearerToken(bearerToken);
		}
	}
}