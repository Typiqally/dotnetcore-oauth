using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NETCore.OAuth.Client.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static async Task<HttpRequestMessage> CloneAsync(this HttpRequestMessage requestMessage)
        {
            var clone = new HttpRequestMessage(requestMessage.Method, requestMessage.RequestUri)
            {
                Content = requestMessage.Content,
                Version = requestMessage.Version
            };

            foreach (var prop in requestMessage.Properties)
            {
                clone.Properties.Add(prop);
            }

            var headers = requestMessage.Headers.Where(x => !x.Key.Equals("Authorization"));
            foreach (var (key, value) in headers)
            {
                clone.Headers.TryAddWithoutValidation(key, value);
            }

            return clone;
        }
    }
}