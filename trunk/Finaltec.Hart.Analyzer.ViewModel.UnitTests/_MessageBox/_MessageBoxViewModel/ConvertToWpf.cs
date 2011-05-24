using System;
using System.Windows.Media.Imaging;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using Finaltec.Hart.Analyzer.ViewModel.MessageBox;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._MessageBox._MessageBoxViewModel
{
    [TestFixture]
    public class ConvertToWpf
    {
        [Test] public void ReturnNull()
        {
            ViewProvider viewProvider = new ViewProvider();
            viewProvider.AddView("MessageBox", typeof(TestView));

            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel(viewProvider, "", "", MessageBoxIcon.None, MessageBoxButtonType.Ok, null);
            BitmapImage image = messageBoxViewModel.ConvertToWpf(null);

            Assert.That(image, Is.Null);
            Assert.That(messageBoxViewModel, Is.Not.Null);
        }
    }
}