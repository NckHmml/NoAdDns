using System;
using System.Collections.Generic;
using System.Text;

namespace NoAdDns.Server.Protocol
{
    public class DnsHeader
    {
        public const int SIZE = 12;

        public ushort Id { get; set; }
        public DnsHeaderFlags Flags { get; set; }
        public ushort QuestionCount { get; set; }
        public ushort AnswerCount { get; set; }
        public ushort AuthorityRecordCount { get; set; }
        public ushort AdditionalRecordCount { get; set; }

        public unsafe DnsHeader(byte[] buffer)
        {
            if (buffer.Length < SIZE)
                throw new ArgumentException("Buffer size is too small", "buffer");

            fixed (byte* pBuffer = buffer)
            {
                ushort* pointer = (ushort*)pBuffer;
                Id = *(pointer + 0);
                Flags = new DnsHeaderFlags(*(pointer + 1));
                QuestionCount = *(pointer + 2);
                AnswerCount = *(pointer + 3);
                AuthorityRecordCount = *(pointer + 4);
                AdditionalRecordCount = *(pointer + 5);
            }
        }
    }
}
