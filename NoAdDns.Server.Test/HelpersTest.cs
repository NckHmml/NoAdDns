using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoAdDns.Server
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void TestIsBitSet()
        {
            ushort toTest = 0x94C0; // 1001 0100 1100 0000
            int[] trueValues = new[] { 0, 3, 5, 8, 9 };
            for (byte i = 0; i < 16; i++)
            {
                if (trueValues.Contains(i))
                    Assert.IsTrue(toTest.IsBitSet(i));
                else
                    Assert.IsFalse(toTest.IsBitSet(i));
            }
        }
    }
}
