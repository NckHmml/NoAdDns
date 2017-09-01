using System;
using System.Collections.Generic;
using System.Text;

namespace NoAdDns.Server.Protocol
{
    public class DnsHeaderFlags
    {
        public bool IsResponse { get; set; } // 0
        public Opcodes Opcode { get; set; } // 1~4
        public bool IsAuthoritative { get; set; } // 5
        public bool IsTruncated { get; set; } // 6
        public bool IsRecursionDesired { get; set; } // 7
        public bool IsRecursionAvailable { get; set; } // 8
        public bool IsAuthenticated { get; set; } // 10
        public bool IsCheckingDisabled { get; set; } // 11
        public ReturnCodes ReturnCode { get; set; } // 12~15

        public DnsHeaderFlags(ushort flags)
        {
            IsResponse = flags.IsBitSet(0);
            Opcode = (Opcodes)flags.GetBitValueAt(1, 4);
            IsAuthoritative = flags.IsBitSet(5);
            IsTruncated = flags.IsBitSet(6);
            IsRecursionDesired = flags.IsBitSet(7);
            IsRecursionAvailable = flags.IsBitSet(8);
            IsAuthenticated = flags.IsBitSet(10);
            IsCheckingDisabled = flags.IsBitSet(11);
            ReturnCode = (ReturnCodes)flags.GetBitValueAt(12, 4);
        }

        public enum Opcodes : ushort
        {
            Query = 0,
            IQuery = 1,
            Status = 2,
            Notify = 4,
            Update = 5
        }

        public enum ReturnCodes : ushort
        {
            NoError = 0,
            FormatError = 1,
            ServerFailure = 2,
            NameError = 3,
            NotImplemented = 4,
            Refused = 5,
            YXDomain = 6,
            YXRRSet = 7,
            NXRRSet = 8,
            NotAuthoritative = 9,
            NotInZone = 10,

            BadSignature = 16,
            BadKey = 17,
            BadTime = 18,
            BadMode = 19,
            BadName = 20,
            BadAlgorithm = 21,
            BadTruncation = 22
        }
    }
}
