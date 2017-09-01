using NoAdDns.Server.Protocol;
using System;

namespace NoAdDns.Server
{
    public class Program
    {
        static DnsServer Server { get; } = new DnsServer();

        static void Main(string[] args)
        {
            while (Server.Running)
            {
                Console.ReadLine();
            }
        }
    }
}
