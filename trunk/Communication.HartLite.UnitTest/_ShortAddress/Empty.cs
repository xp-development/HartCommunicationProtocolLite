using NUnit.Framework;

namespace Communication.HartLite.UnitTest._ShortAddress
{
    [TestFixture]
    public class Empty
    {
        [Test]
        public void Usage()
        {
            var address = ShortAddress.Empty;

            Assert.That(address, Is.Not.Null);
            Assert.That(address.ToByteArray(), Is.EqualTo(new byte[] { 0x80 }));
        }
    }
}