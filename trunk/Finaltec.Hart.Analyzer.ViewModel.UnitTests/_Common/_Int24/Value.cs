using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._Int24
{
    [TestFixture]
    public class Value
    {
        [Test]
        public void Usage()
        {
            Int24 int24 = new Int24 { Value = 255 };
            Assert.That(int24.Value, Is.EqualTo(255));  
        }

        [Test]
        public void WillThrowArgumentException()
        {
            Int24 int24 = new Int24();
            ArgumentException exception = Assert.Throws<ArgumentException>(() => int24.Value = 8388608);

            Assert.That(exception.Message, Is.EqualTo("Value was either too large or too small for an Int24."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(ArgumentException)));

            exception = Assert.Throws<ArgumentException>(() => int24.Value = -8388609);

            Assert.That(exception.Message, Is.EqualTo("Value was either too large or too small for an Int24."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(ArgumentException)));  
        }
    }
}