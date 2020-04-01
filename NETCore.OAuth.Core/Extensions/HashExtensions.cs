using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NETCore.OAuth.Core.Extensions
{
    public static class HashExtensions
    {
        public static string ToSha256(this string value)
        {
            if (value == null) return null;

            using var hash = SHA256.Create();
            return string.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                .Select(item => item.ToString("x2")));
        }
    }
}