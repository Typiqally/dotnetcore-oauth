namespace NETCore.OAuth.Core.Pkce
{
    public class PkcePair
    {
        public string Verifier { get; set; }
        
        public string Challenge { get; set; }
        
        public CodeChallengeMethod Method { get; set; }
    }
}