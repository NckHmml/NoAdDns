using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoAdDns.Server.Protocol
{
    public class DnsQuestion
    {
        public int Size { get; private set; }

        public DnsQuestion(byte[] message, int offset)
        {
            // [[byte length] [string(length) label]] 00
            // First get the total length and the label count of the domain
            byte size = 0;
            int length = 0;
            int labels = -1;
            int tOffset = offset;
            do
            {
                size = message[tOffset];
                if (message.Length < tOffset + size + 1)
                    throw new ArgumentException("Invalid request message", "message");
                length += size;
                labels++;
                tOffset += size + 1;
            } while (size != 0);

            // Create buffer to write the string into
            length += labels - 1;
            byte[] buffer = new byte[length];

            // Construct the string to the buffer
            tOffset = offset;
            int sOffset = 0;
            while (labels > 0)
            {
                size = message[tOffset];
                Buffer.BlockCopy(message, tOffset + 1, buffer, sOffset, size);
                // Check and add separator
                if (--labels > 0)
                    buffer[sOffset + size] = (byte)'.';

                tOffset += size + 1;
                sOffset = tOffset - offset;
            }

            // Conver the buffer to a string
            char[] chars = buffer.Select(x => (char)x).ToArray();
            var domain = new string(chars);
        }
    }
}
