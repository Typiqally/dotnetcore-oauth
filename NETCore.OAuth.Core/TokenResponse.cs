using System.Text.Json.Serialization;

namespace NETCore.OAuth.Core
{
	public class TokenResponse
	{
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; }
		
		[JsonPropertyName("token_type")]
		public TokenType Type { get; set; }
		
		[JsonPropertyName("expires_in")]
		public int ExpiresIn { get; set; }
		
		[JsonPropertyName("refresh_token")]
		public string RefreshToken { get; set; }
	}
}