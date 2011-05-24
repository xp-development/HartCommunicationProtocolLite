using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._SettingsDialogModel
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            ViewProvider viewProvider = new ViewProvider();
            viewProvider.AddView("SettingsView", typeof(TestView));

            SettingsDialogModel settingsDialogModel = new SettingsDialogModel(viewProvider);
            Assert.That(settingsDialogModel, Is.Not.Null);
        }
    }
}