using System;
using System.Text;

namespace NETCore.OAuth.Core.Extensions
{
    public static class Base64UrlExtensions
    {
        private static readonly char[] Padding = { '=' };
        
        public static string ToBase64Url(this string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            
            return Convert.ToBase64String(bytes)
                .TrimEnd(Padding)
                    .Replace('+', '-')
                    .Replace('/', '_');
        }
    }
}