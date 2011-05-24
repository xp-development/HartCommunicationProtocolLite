using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._InterfaceIsNotImplementedException
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            InterfaceIsNotImplementedException interfaceIsNotImplementedException = new InterfaceIsNotImplementedException("UNIT TEST");
            NotImplementedException notImplementedException = interfaceIsNotImplementedException;

            Assert.That(interfaceIsNotImplementedException.Message, Is.EqualTo("UNIT TEST"));
            Assert.That(interfaceIsNotImplementedException.GetType(), Is.EqualTo(typeof(InterfaceIsNotImplementedException)));

            Assert.That(notImplementedException, Is.Not.Null);
            Assert.That(notImplementedException.Message, Is.EqualTo("UNIT TEST"));  
        }
    }
}