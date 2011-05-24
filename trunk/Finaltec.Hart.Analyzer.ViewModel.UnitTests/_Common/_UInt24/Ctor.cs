using System;
using System.Globalization;
using System.Threading;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._UInt24
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            UInt24 int24 = new UInt24(10);
            Assert.That(int24.Value, Is.EqualTo(10));  

            Int24 secInt24 = new Int24();
            Assert.That(secInt24.Value, Is.EqualTo(0));
            secInt24.Value = 5;
            Assert.That(secInt24.Value, Is.EqualTo(5));  
        }

        [Test]
        public void WillThrowArgumentException()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            ArgumentException exception = Assert.Throws<ArgumentException>(() => new UInt24(999999999));

            Assert.That(exception.Message, Is.EqualTo("Value was either too large or too small for an UInt24."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(ArgumentException)));
        }
    }
}