using System;

namespace NETCore.OAuth.Core
{
	public class DiscoveryDocument
	{
		public Uri AuthorizeEndpoint { get; set; }
		public Uri TokenEndpoint { get; set; }
	}
}