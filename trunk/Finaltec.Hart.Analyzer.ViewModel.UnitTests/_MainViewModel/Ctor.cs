using Finaltec.Hart.Analyzer.ViewModel.Common;
using Finaltec.Hart.Analyzer.ViewModel.DataModels;
using Finaltec.Hart.Analyzer.ViewModel.DataTemplate;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._MainViewModel
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            MainViewModel mainViewModel = new MainViewModel(new ViewProvider());
            Assert.That(mainViewModel, Is.Not.Null);

            Assert.That(mainViewModel.IsConnected, Is.False);
            Assert.That(mainViewModel.DataTransferModel, Is.EqualTo(DataTransferModel.GetInstance()));
            Assert.That(mainViewModel.Filter.DisplayTime, Is.EqualTo(new Filter().DisplayTime));
            Assert.That(mainViewModel.Value, Is.EqualTo(""));
            Assert.That(mainViewModel.SelectedOutput, Is.EqualTo(""));

            Assert.That(mainViewModel.ConnectDisconnectCommand, Is.Not.Null);
            Assert.That(mainViewModel.DisplayConnectionSettingsCommand, Is.Not.Null);
            Assert.That(mainViewModel.SendCommand, Is.Not.Null);
        }
    }
}