using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._UInt24
{
    [TestFixture]
    public class Value
    {
        [Test]
        public void Usage()
        {
            UInt24 int24 = new UInt24 { Value = 255 };
            Assert.That(int24.Value, Is.EqualTo(255));
        }

        [Test]
        public void WillThrowArgumentException()
        {
            UInt24 int24 = new UInt24();
            ArgumentException exception = Assert.Throws<ArgumentException>(() => int24.Value = 16777216);

            Assert.That(exception.Message, Is.EqualTo("Value was either too large or too small for an UInt24."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(ArgumentException)));
        }
    }
}