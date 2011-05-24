using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._ViewProvider
{
    [TestFixture]
    public class AddGetView
    {
        [Test]
        public void Usage()
        {
            ViewProvider viewProvider = new ViewProvider();
            Assert.That(viewProvider, Is.Not.Null);

            viewProvider.AddView("TestView", typeof(TestView));
            IView view = viewProvider.GetView("TestView", this);
            Assert.That(view, Is.Not.Null);
        }
    }
}