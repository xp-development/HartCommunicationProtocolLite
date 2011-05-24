using System;
using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._LongAddress
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void ShouldFailIfDeviceIdentificationNumberHasNotThreeBytes()
        {
            byte manufacturerIdentificationCode = 0xDE;
            byte manufacturerDeviceTypeCode = 0x17;
            byte[] deviceIdentificationNumber = new byte[] { 0x00 };

            Assert.Throws<ArgumentException>(() => new LongAddress(manufacturerIdentificationCode, manufacturerDeviceTypeCode, deviceIdentificationNumber));
        }
    }
}