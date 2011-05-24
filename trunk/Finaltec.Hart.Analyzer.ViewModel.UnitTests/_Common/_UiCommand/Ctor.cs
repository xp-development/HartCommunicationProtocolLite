using System;
using System.Globalization;
using System.Threading;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._UiCommand
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            UiCommand command = new UiCommand(NotImplementetCommandExecute);
            Assert.That(command, Is.Not.Null);
            Assert.That(command.CanExecute(null), Is.True);
        }

        [Test]
        public void CtorFail()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new UiCommand(null));

            Assert.That(exception.Message, Is.EqualTo("Comamnds can not be null. Please set a valid value.\r\nParameter name: objExecuteMethod"));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(ArgumentNullException)));  
        }

        private static void NotImplementetCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }
    }
}