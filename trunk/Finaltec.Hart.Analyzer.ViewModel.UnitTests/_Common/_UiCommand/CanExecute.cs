using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._UiCommand
{
    [TestFixture]
    public class CanExecute
    {
        private UiCommand _command;
        private bool _canExecute = true;

        [Test]
        public void Usage()
        {
            _command = new UiCommand(TestCommandExecute, obj => _canExecute);
            Assert.That(_command, Is.Not.Null);
            Assert.That(_command.CanExecute(null), Is.True);

            _command.Execute(false);
            Assert.That(_command.CanExecute(null), Is.False);
        }

        private void TestCommandExecute(object obj)
        {
            _canExecute = obj is bool ? (bool) obj : false;
            _command.InvokeCanExecuteChanged();
        }
    }
}