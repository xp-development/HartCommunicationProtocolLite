using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._ViewProvider
{
    [TestFixture]
    public class GetView
    {
        [Test]
        public void WillThrowArgumentException()
        {
            ViewProvider viewProvider = new ViewProvider();
            ArgumentException exception = Assert.Throws<ArgumentException>(() => viewProvider.GetView("SomeView"));

            Assert.That(exception.Message, Is.EqualTo("ViewID does not exists."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(ArgumentException)));  
        }
    }
}