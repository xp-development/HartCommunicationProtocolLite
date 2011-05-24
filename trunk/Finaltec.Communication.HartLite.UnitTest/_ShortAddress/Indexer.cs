using System;
using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._ShortAddress
{
    [TestFixture]
    public class Indexer
    {
        [Test]
        public void Usage()
        {
            ShortAddress address = new ShortAddress(1);

            Assert.That(address[0], Is.EqualTo(129));
        }

        [Test]
        public void ShouldFailIfIndexIsHigherFourOrLessZero()
        {
            ShortAddress address = new ShortAddress(1);

            Assert.Throws<IndexOutOfRangeException>(() => Console.WriteLine(address[1]));
            Assert.Throws<IndexOutOfRangeException>(() => Console.WriteLine(address[-1]));
        }
    }
}