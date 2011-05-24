using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._Int24
{
    [TestFixture]
    public class ToByteArray
    {
        [Test]
        public void Usage()
        {
            Int24 int24 = new Int24 { Value = 32647 };
            byte[] byteArray = int24.ToByteArray();

            Assert.That(byteArray[0], Is.EqualTo(135));
            Assert.That(byteArray[1], Is.EqualTo(127));
            Assert.That(byteArray[2], Is.EqualTo(0));  
        }
    }
}