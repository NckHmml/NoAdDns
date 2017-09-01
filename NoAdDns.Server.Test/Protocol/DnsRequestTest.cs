using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoAdDns.Server.Protocol
{
    [TestClass]
    public class DnsRequestTest
    {
        [TestMethod]
        public void TestParse()
        {
            var message = new byte[]
            {
                0x00, 0x00,
                0x00, 0x00,
                0x01, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,

                // www.mydomain.com
                0x03, 0x77, 0x77, 0x77, 0x08, 0x6D, 0x79, 0x64, 0x6F, 0x6D, 0x61, 0x69, 0x6E, 0x03, 0x63, 0x6F, 0x6D, 0x00
            };
            var request = new DnsRequest(message);
        }
    }
}
