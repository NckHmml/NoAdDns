using System;
using System.Collections.Generic;
using System.Text;

namespace NoAdDns.Server
{
    public static class Helpers
    {
        /// <summary>
        /// Gets the bitwise value at a certain position and length
        /// </summary>
        /// <param name="this">Object to get value of</param>
        /// <param name="offset">Offset to start at</param>
        /// <param name="length">Amount of bytes to read</param>
        /// <remarks>BigEndian</remarks>
        public static ushort GetBitValueAt(this ushort @this, byte offset, byte length)
        {
            const int size = sizeof(ushort) * 0x08 - 1;
            return (byte)((@this >> (size - offset) & ~(0xff << length)));
        }

        /// <summary>
        /// Gets the bitwise value at a certain position
        /// </summary>
        /// <param name="this">Object to get value of</param>
        /// <param name="offset">Offset to get value at</param>
        /// <remarks>BigEndian</remarks>
        public static ushort GetBitValueAt(this ushort @this, byte offset)
        {
            return @this.GetBitValueAt(offset, 1);
        }

        /// <summary>
        /// Checks if a bit is set at a certain position
        /// </summary>
        /// <param name="this">Object to get value of</param>
        /// <param name="offset">Offset to get value at</param>
        /// <remarks>BigEndian</remarks>
        public static bool IsBitSet(this ushort @this, byte offset)
        {
            return @this.GetBitValueAt(offset) == 1;
        }
    }
}
