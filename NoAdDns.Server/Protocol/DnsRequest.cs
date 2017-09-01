using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoAdDns.Server.Protocol
{
    public class DnsRequest
    {
        public DnsRequest() { }

        public DnsRequest(byte[] message)
        {
            // Parse header
            var header = new DnsHeader(message);

            // Check for behaviour that is normally a response
            if (header.Flags.IsResponse ||
                header.QuestionCount == 0 ||
                (header.AdditionalRecordCount & header.AnswerCount & header.AuthorityRecordCount) > 0 ||
                header.Flags.ReturnCode != DnsHeaderFlags.ReturnCodes.NoError)
            {
                throw new ArgumentException("Invalid request message", "message");
            }

            ParseQuestions(header, message).ToList();
        }

        private IEnumerable<DnsQuestion> ParseQuestions(DnsHeader header, byte[] message)
        {
            int offset = DnsHeader.SIZE;
            if (header.QuestionCount > 0 && message.Length <= offset)
                throw new ArgumentException("Invalid request message", "message");

            for (int i = 0; i < header.QuestionCount; i++)
            {
                var question = new DnsQuestion(message, offset);
                yield return question;
                offset += question.Size;
            }
        }
    }
}
