using System;
using System.Globalization;
using System.Threading;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._ViewProvider
{
    [TestFixture]
    public class AddView
    {
        [Test]
        public void WillThrowArgumentNullExceptionWhileViewId()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            ViewProvider viewProvider = new ViewProvider();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => viewProvider.AddView(null, typeof(TestView)));

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: viewId"));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(ArgumentNullException)));  
        }

        [Test]
        public void WillThrowArgumentNullExceptionWhileViewType()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            ViewProvider viewProvider = new ViewProvider();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => viewProvider.AddView("View", null));

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: viewType"));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(ArgumentNullException)));  
        }

        [Test]
        public void WillThrowInterfaceIsNotImplementedException()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            ViewProvider viewProvider = new ViewProvider();
            InterfaceIsNotImplementedException exception = Assert.Throws<InterfaceIsNotImplementedException>(() => viewProvider.AddView("View", typeof(AddView)));

            Assert.That(exception.Message, Is.EqualTo("Type does not implement interface IView."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(InterfaceIsNotImplementedException)));  
        }
    }
}