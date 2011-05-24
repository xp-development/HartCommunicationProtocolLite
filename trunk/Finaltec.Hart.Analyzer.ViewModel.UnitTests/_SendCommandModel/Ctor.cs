using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._SendCommandModel
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            ViewProvider viewProvider = new ViewProvider();
            viewProvider.AddView("SendCommand", typeof(TestView));

            SendCommandModel sendCommandModel = new SendCommandModel(viewProvider);
            Assert.That(sendCommandModel, Is.Not.Null);
        }
    }
}