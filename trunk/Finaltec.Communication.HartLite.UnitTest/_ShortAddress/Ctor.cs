using System;
using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._ShortAddress
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void ShouldFailIfPollingAddressIsOutOfRange()
        {
            Assert.Throws<ArgumentException>(() => new ShortAddress(16));
        }
    }
}