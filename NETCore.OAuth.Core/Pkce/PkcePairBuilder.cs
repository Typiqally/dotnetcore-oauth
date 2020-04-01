using System;
using System.Text;
using NETCore.OAuth.Core.Extensions;

namespace NETCore.OAuth.Core.Pkce
{
    public class PkcePairBuilder
    {
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        private readonly CodeChallengeMethod _method;
        private readonly int _length;

        private readonly Random _random = new Random();

        public PkcePairBuilder(CodeChallengeMethod method = CodeChallengeMethod.Sha256, int length = 48)
        {
            _method = method;
            _length = length;
        }

        public PkcePair Build()
        {
            var verifier = CreateVerifier();
            var challenge = CreateChallenge(verifier);

            return new PkcePair
            {
                Verifier = verifier,
                Challenge = challenge,
                Method = _method
            };
        }

        private string CreateChallenge(string verifier)
        {
            var hash = _method switch
            {
                CodeChallengeMethod.PlainText => verifier,
                CodeChallengeMethod.Sha256 => verifier.ToSha256(),
                _ => throw new ArgumentOutOfRangeException(nameof(_method))
            };

            return hash.ToBase64Url();
        }

        private string CreateVerifier()
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < _length; i++)
            {
                stringBuilder.Append(Characters[_random.Next(Characters.Length)]);
            }

            return stringBuilder.ToString();
        }
    }
}