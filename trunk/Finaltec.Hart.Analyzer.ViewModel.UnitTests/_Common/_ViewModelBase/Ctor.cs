using System;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._ViewModelBase
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            ViewModelBase viewModelBase = new ViewModelBase(new ViewProvider());
            Assert.That(viewModelBase, Is.Not.Null);
            Assert.That(viewModelBase.ViewProvider, Is.Not.Null);
        }
    }
}