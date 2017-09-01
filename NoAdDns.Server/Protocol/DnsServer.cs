using NoAdDns.Server.Protocol;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NoAdDns.Server.Protocol
{
    public class DnsServer
    {
        private const int DEFAULT_PORT = 53;
        private UdpClient Listener { get; }

        public bool Running { get; private set; } = true;

        public DnsServer(int port = DEFAULT_PORT)
        {
            Listener = new UdpClient(port);
        }

        public async Task Listen()
        {
            await Task.Yield();

            while (Running)
            {
                UdpReceiveResult result;
                try
                {
                    result = await Listener.ReceiveAsync();
                }
                catch (Exception)
                {
                    // Todo: Log
                }
                HandleRequest(result);
            }
        }

        private async void HandleRequest(UdpReceiveResult result)
        {
            try
            {
                // Parse the request
                var query = new DnsRequest(result.Buffer);
            }
            catch (Exception)
            {
                // Todo: Log
            }
        }
    }
}
