using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._MainViewModel
{
    [TestFixture]
    public class InitView
    {
        [Test]
        public void Usage()
        {
            ViewProvider viewProvider = new ViewProvider();
            viewProvider.AddView("SettingsView", typeof(TestView));
            viewProvider.AddView("MainView", typeof(TestView));

            MainViewModel mainViewModel = new MainViewModel(viewProvider);
            Assert.That(mainViewModel.ViewProvider, Is.Not.Null);

            mainViewModel.InitView();

            Assert.That(TestView.WasShowDialog, Is.True);  
            Assert.That(TestView.WasShow, Is.True);
        }
    }
}