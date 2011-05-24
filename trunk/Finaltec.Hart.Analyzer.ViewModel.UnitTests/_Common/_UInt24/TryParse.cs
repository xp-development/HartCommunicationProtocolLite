using System.Globalization;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._UInt24
{
    [TestFixture]
    public class TryParse
    {
        [Test]
        public void Usage()
        {
            UInt24 int24;
            bool parseResult = UInt24.TryParse("255", NumberStyles.Integer, null, out int24);

            Assert.That(parseResult, Is.True);
            Assert.That(int24.Value, Is.EqualTo(255));
        }

        [Test]
        public void FailOnWrongFormat()
        {
            UInt24 int24;
            bool parseResult = UInt24.TryParse("WILL FAIL", NumberStyles.None, null, out int24);

            Assert.That(parseResult, Is.False);
            Assert.That(int24.Value, Is.EqualTo(0));
        }

        [Test]
        public void FailOnOverflow()
        {
            UInt24 int24;
            bool parseResult = UInt24.TryParse("16777216", NumberStyles.Integer, null, out int24);

            Assert.That(parseResult, Is.False);
            Assert.That(int24.Value, Is.EqualTo(0));
        }
    }
}