using System;
using System.Globalization;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._Int24
{
    [TestFixture]
    public class TryParse
    {
        [Test]
        public void Usage()
        {
            Int24 int24;
            bool parseResult = Int24.TryParse("255", NumberStyles.Integer, null, out int24);

            Assert.That(parseResult, Is.True);
            Assert.That(int24.Value, Is.EqualTo(255));  
        }

        [Test]
        public void FailOnWrongFormat()
        {
            Int24 int24;
            bool parseResult = Int24.TryParse("WILL FAIL", NumberStyles.None, null, out int24);

            Assert.That(parseResult, Is.False);
            Assert.That(int24.Value, Is.EqualTo(0));  
        }

        [Test]
        public void FailOnOverflow()
        {
            Int24 int24;
            bool parseResult = Int24.TryParse("8388608", NumberStyles.Integer, null, out int24);

            Assert.That(parseResult, Is.False);
            Assert.That(int24.Value, Is.EqualTo(0));  
        }
    }
}