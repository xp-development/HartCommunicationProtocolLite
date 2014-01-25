using System;
using NUnit.Framework;

namespace Communication.HartLite.UnitTest._LongAddress
{
    [TestFixture]
    public class Indexer
    {
        [Test]
        public void Usage()
        {
            byte manufacturerIdentificationCode = 0xDE;
            byte manufacturerDeviceTypeCode = 0x17;
            byte[] deviceIdentificationNumber = new byte[] { 0x00, 0x00, 0x01 };

            LongAddress address = new LongAddress(manufacturerIdentificationCode, manufacturerDeviceTypeCode, deviceIdentificationNumber);

            Assert.That(address[0], Is.EqualTo(0x9E));
            Assert.That(address[1], Is.EqualTo(0x17));
            Assert.That(address[2], Is.EqualTo(0x00));
            Assert.That(address[3], Is.EqualTo(0x00));
            Assert.That(address[4], Is.EqualTo(0x01));
        }

        [Test]
        public void ShouldFailIfIndexIsHigherFourOrLessZero()
        {
            byte manufacturerIdentificationCode = 0xDE;
            byte manufacturerDeviceTypeCode = 0x17;
            byte[] deviceIdentificationNumber = new byte[] { 0x00, 0x00, 0x01 };

            LongAddress address = new LongAddress(manufacturerIdentificationCode, manufacturerDeviceTypeCode, deviceIdentificationNumber);

            Assert.Throws<IndexOutOfRangeException>(() => Console.WriteLine(address[5]));
            Assert.Throws<IndexOutOfRangeException>(() => Console.WriteLine(address[-1]));
        }
    }
}