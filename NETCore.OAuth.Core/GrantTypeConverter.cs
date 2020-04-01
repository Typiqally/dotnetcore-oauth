using System;

namespace NETCore.OAuth.Core
{
	public class GrantTypeConverter : IEnumConverter<GrantType>
	{
		public string ConvertToString(GrantType type)
		{
			return type switch
			{
				GrantType.AuthorizationCode => "authorization_code",
				GrantType.Password => "password",
				GrantType.ClientCredentials => "client_credentials",
				GrantType.Implicit => "implicit",
				GrantType.RefreshToken => "refresh_token",
				_ => throw new ArgumentOutOfRangeException(nameof(type))
			};
		}

		public GrantType FromString(string str)
		{
			throw new NotImplementedException();
		}
	}
}