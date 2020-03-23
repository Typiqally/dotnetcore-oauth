using System;

namespace NETCore.OAuth.Core.Extensions
{
	public static class UriExtensions
	{
		public static Uri Normalize(this Uri uri)
		{
			return new Uri(uri + "/");
		}
	}
}