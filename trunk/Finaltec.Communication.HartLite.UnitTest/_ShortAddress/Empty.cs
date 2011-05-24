using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._ShortAddress
{
    [TestFixture]
    public class Empty
    {
        [Test]
        public void Usage()
        {
            ShortAddress address = ShortAddress.Empty;

            Assert.That(address, Is.Not.Null);
            Assert.That(address.ToByteArray(), Is.EqualTo(new byte[] { 0x80 }));
        }
    }
}