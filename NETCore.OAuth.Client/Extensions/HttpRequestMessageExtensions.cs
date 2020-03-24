using System.Collections.Generic;
using System.IO;
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
                Version = requestMessage.Version
            };

            var stream = new MemoryStream();
            if (requestMessage.Content != null)
            {
                await requestMessage.Content
                    .CopyToAsync(stream)
                    .ConfigureAwait(false);

                stream.Position = 0;
                clone.Content = new StreamContent(stream);

                if (requestMessage.Content.Headers != null)
                {
                    foreach (var (key, value) in requestMessage.Content.Headers)
                    {
                        clone.Content.Headers.Add(key, value);
                    }
                }
            }

            foreach (var prop in requestMessage.Properties)
            {
                clone.Properties.Add(prop);
            }

            foreach (var (key, value) in requestMessage.Headers)
            {
                clone.Headers.TryAddWithoutValidation(key, value);
            }

            return clone;
        }
    }
}