using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace NETCore.OAuth.Client.Extensions
{
    public static class ByteArrayExtensions
    {
        public static IEnumerable<byte> RemoveTrailingNulls(this IEnumerable<byte> bytes)
        {
            var enumerable = bytes as byte[] ?? bytes.ToArray();
            return enumerable.TakeWhile((v, index) => enumerable.Skip(index).Any(w => w != 0x00));
        }

        public static HttpRequestMessage ToRequestMessage(this IEnumerable<byte> bytes)
        {
            var enumerable = bytes.ToList();
            
            var split = enumerable
                .RemoveTrailingNulls()
                .Split((byte) 13, 2)
                .ToList();

            var encoded = split.Select(b => Encoding.ASCII.GetString(b.ToArray())).ToList();

            var request = encoded.ElementAt(0).Split(" ");
            var method = new HttpMethod(request[0]);
            var requestUri = request[1];

            var requestMessage = new HttpRequestMessage(method, requestUri);

            foreach (var header in encoded.Skip(1).Select(header => header.Split(": ")))
            {
                requestMessage.Headers.TryAddWithoutValidation(header[0], header[1]);
            }

            return requestMessage;
        }
    }
}