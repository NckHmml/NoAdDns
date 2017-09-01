using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoAdDns.Server.Protocol
{
    public class DnsRequest
    {
        protected byte[] Message { get; set; }
        public DnsHeader Header { get;  }

        public DnsRequest() { }

        public DnsRequest(byte[] message)
        {
            Message = message;
            Header = ParseHeader();
            ParseQuestions().ToList();
        }

        protected DnsHeader ParseHeader()
        {
            // Parse header
            var header = new DnsHeader(Message);

            // Check for behaviour that is normally a response
            if (header.Flags.IsResponse ||
                header.QuestionCount == 0 ||
                (header.AdditionalRecordCount & header.AnswerCount & header.AuthorityRecordCount) > 0 ||
                header.Flags.ReturnCode != DnsHeaderFlags.ReturnCodes.NoError)
            {
                throw new ArgumentException("Invalid request message", "message");
            }

            return header;
        }

        protected IEnumerable<DnsQuestion> ParseQuestions()
        {
            int offset = DnsHeader.SIZE;
            if (Header.QuestionCount > 0 && Message.Length <= offset)
                throw new ArgumentException("Invalid request message", "message");

            for (int i = 0; i < Header.QuestionCount; i++)
            {
                var question = new DnsQuestion(Message, offset);
                yield return question;
                offset += question.Size;
            }
        }
    }
}
