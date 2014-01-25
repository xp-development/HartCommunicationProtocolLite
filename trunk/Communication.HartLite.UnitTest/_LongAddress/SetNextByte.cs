using NUnit.Framework;

namespace Communication.HartLite.UnitTest._LongAddress
{
    [TestFixture]
    public class SetNextByte
    {
        [Test]
        public void Usage()
        {
            var address = LongAddress.Empty;

            byte manufacturerIdentificationCode = 0xDE;
            byte manufacturerDeviceTypeCode = 0x17;
            byte[] deviceIdentificationNumber = new byte[] { 0x00, 0x00, 0x01 };

            address.SetNextByte(manufacturerIdentificationCode);
            address.SetNextByte(manufacturerDeviceTypeCode);
            address.SetNextByte(deviceIdentificationNumber[0]);
            address.SetNextByte(deviceIdentificationNumber[1]);
            address.SetNextByte(deviceIdentificationNumber[2]);

            Assert.That(address[0], Is.EqualTo(0x9E));
            Assert.That(address[1], Is.EqualTo(0x17));
            Assert.That(address[2], Is.EqualTo(0x00));
            Assert.That(address[3], Is.EqualTo(0x00));
            Assert.That(address[4], Is.EqualTo(0x01));
        }
    }
}