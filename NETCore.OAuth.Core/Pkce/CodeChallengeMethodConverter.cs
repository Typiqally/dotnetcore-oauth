using System;

namespace NETCore.OAuth.Core.Pkce
{
    public class CodeChallengeMethodConverter : IEnumConverter<CodeChallengeMethod>
    {
        public string ConvertToString(CodeChallengeMethod method)
        {
            return method switch
            {
                CodeChallengeMethod.PlainText => "plain",
                CodeChallengeMethod.Sha256 => "S256",
                _ => throw new ArgumentOutOfRangeException(nameof(method))
            };
        }

        public CodeChallengeMethod FromString(string str)
        {
            throw new System.NotImplementedException();
        }
    }
}