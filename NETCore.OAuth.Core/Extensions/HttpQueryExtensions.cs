using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.OAuth.Core.Extensions
{
	public static class HttpQueryExtensions
	{
		public static string AppendQueryParameters(this string str, Dictionary<string, string> parameters)
		{
			var uri = str;

			if (!uri.EndsWith("?"))
				uri += "?";

			return uri + string.Join("&", parameters.Select(pair => $"{pair.Key}={pair.Value}"));
		}

		public static Uri AppendQueryParameters(this Uri uri, Dictionary<string, string> parameters)
		{
			return new Uri(uri.ToString().AppendQueryParameters(parameters));
		}
	}
}