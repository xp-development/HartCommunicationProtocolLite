using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._UiCommand
{
    [TestFixture]
    public class Execute
    {
        [Test]
        public void Usage()
        {
            UiCommand command = new UiCommand(TestCommandExecute);
            Assert.That(command, Is.Not.Null);

            NotImplementedException exception = Assert.Throws<NotImplementedException>(() => command.Execute(null));
            Assert.That(exception.Message, Is.EqualTo("Command was Executet but is not implementet."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(NotImplementedException)));  
        }

        private static void TestCommandExecute(object obj)
        {
            throw new NotImplementedException("Command was Executet but is not implementet.");
        }
    }
}