using System;
using System.ComponentModel;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._ViewModelBase
{
    [TestFixture]
    public class InvokePropertyChanged
    {
        private bool _propertyChanged;

        [Test]
        public void Usage()
        {
            ViewModelBase viewModelBase = new ViewModelBase(new ViewProvider());
            Assert.That(viewModelBase, Is.Not.Null);

            viewModelBase.PropertyChanged += PropertyChangedEventHandle;
            Assert.That(_propertyChanged, Is.False);

            viewModelBase.InvokePropertyChanged("ViewProvider");
            viewModelBase.PropertyChanged -= PropertyChangedEventHandle;

            Assert.That(_propertyChanged, Is.True);
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            _propertyChanged = true;
        }
    }
}