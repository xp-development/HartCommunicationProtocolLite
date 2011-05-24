using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._ViewProvider
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            ViewProvider viewProvider = new ViewProvider();
            Assert.That(viewProvider, Is.Not.Null);
        }
    }
}