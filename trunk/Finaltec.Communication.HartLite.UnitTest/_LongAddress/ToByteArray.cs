using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._LongAddress
{
    [TestFixture]
    public class ToByteArray
    {
        [Test]
        public void Usage()
        {
            byte manufacturerIdentificationCode = 0xDE;
            byte manufacturerDeviceTypeCode = 0x17;
            byte[] deviceIdentificationNumber = new byte[] { 0x00, 0x00, 0x01 };

            byte[] address = new LongAddress(manufacturerIdentificationCode, manufacturerDeviceTypeCode, deviceIdentificationNumber).ToByteArray();

            Assert.That(address[0], Is.EqualTo(0x9E));
            Assert.That(address[1], Is.EqualTo(0x17));
            Assert.That(address[2], Is.EqualTo(0x00));
            Assert.That(address[3], Is.EqualTo(0x00));
            Assert.That(address[4], Is.EqualTo(0x01));
        }
    }
}