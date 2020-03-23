using System.Text.Json.Serialization;

namespace NETCore.OAuth.Core
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum TokenType
	{
		Bearer
	}
}