using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NoAdDns.Server.Protocol
{
    [TestClass]
    public class DnsRequestTest : DnsRequest
    {
        [TestMethod]
        public void TestParseHeader()
        {
            // Valid message, single question
            Message = new byte[]
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
            DnsHeader header = ParseHeader();
            Assert.AreEqual(1, header.QuestionCount);
            Assert.AreEqual(0, header.AnswerCount);
            Assert.AreEqual(0, header.AuthorityRecordCount);

            // Invalid message, no question data
            Message = new byte[]
            {
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
            };
            Assert.ThrowsException<ArgumentException>(() => ParseHeader());

            // Invalid message, IsResponse
            Message = new byte[]
            {
                0x00, 0x00,
                0x00, 0x80,
                0x01, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
            };
            Assert.ThrowsException<ArgumentException>(() => ParseHeader());

            // Invalid message, Invalid ReturnCode
            Message = new byte[]
            {
                0x00, 0x00,
                0x0F, 0x00,
                0x01, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
            };
            Assert.ThrowsException<ArgumentException>(() => ParseHeader());
        }
    }
}
