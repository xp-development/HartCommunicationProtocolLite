using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._ShortAddress
{
    [TestFixture]
    public class SetNextByte
    {
        [Test]
        public void Usage()
        {
            ShortAddress address = ShortAddress.Empty;

            address.SetNextByte(1);

            Assert.That(address[0], Is.EqualTo(129));
        }
    }
}