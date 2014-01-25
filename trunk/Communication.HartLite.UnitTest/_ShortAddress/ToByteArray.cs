using NUnit.Framework;

namespace Communication.HartLite.UnitTest._ShortAddress
{
    [TestFixture]
    public class ToByteArray
    {
        [Test]
        public void Usage()
        {
            var address = new ShortAddress(1);

            byte[] byteArray = address.ToByteArray();

            Assert.That(byteArray.Length, Is.EqualTo(1));
            Assert.That(byteArray[0], Is.EqualTo(129));
        }
    }
}