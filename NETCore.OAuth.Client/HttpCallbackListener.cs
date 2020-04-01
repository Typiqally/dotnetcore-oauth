using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NETCore.OAuth.Client.Extensions;

namespace NETCore.OAuth.Client
{
    public class HttpCallbackListener
    {
        private const string ResponseMessage = "<script type=\"text/javascript\">window.open(window.location, '_self').close();</script>";

        private readonly int _port;
        private readonly int _bufferSize;
        private readonly TcpListener _listener;

        private bool _listening;

        public HttpCallbackListener(int port, int bufferSize = 2048)
        {
            _port = port;
            _bufferSize = bufferSize;

            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
        }

        public async Task ListenAsync(Action<HttpRequestMessage> callback)
        {
            _listening = true;
            _listener.Start();

            while (_listening)
            {
                var client = await _listener.AcceptSocketAsync();
                var buffer = new byte[_bufferSize];

                while (client.Available > 0)
                {
                    client.Receive(buffer);
                }

                client.Send(Encoding.ASCII.GetBytes(ResponseMessage));
                client.Close();

                callback(buffer.ToRequestMessage());
            }

            _listener.Stop();
        }

        public void Stop()
        {
            _listening = false;
        }
    }
}