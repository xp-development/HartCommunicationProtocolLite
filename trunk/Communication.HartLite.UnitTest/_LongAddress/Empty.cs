using NUnit.Framework;

namespace Communication.HartLite.UnitTest._LongAddress
{
    [TestFixture]
    public class Empty
    {
        [Test]
        public void Usage()
        {
            var address = LongAddress.Empty;

            Assert.That(address, Is.Not.Null);
            Assert.That(address.ToByteArray(), Is.EqualTo(new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00}));
        }
    }
}