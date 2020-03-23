using System.Collections.Generic;

namespace NETCore.OAuth.Core
{
	public class ClientCredentials
	{
		public string ClientId { get; set; }

		public string ClientSecret { get; set; }

		public string RedirectUri { get; set; }

		public string Scope { get; set; }

		public Dictionary<string, string> Dictionary() => new Dictionary<string, string>()
		{
			{"client_id", ClientId},
			{"client_secret", ClientSecret},
			{"redirect_uri", RedirectUri},
			{"scope", Scope}
		};
	}
}