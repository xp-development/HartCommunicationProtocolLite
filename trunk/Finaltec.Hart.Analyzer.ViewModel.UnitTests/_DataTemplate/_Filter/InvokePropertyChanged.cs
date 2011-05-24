using System;
using System.ComponentModel;
using Finaltec.Hart.Analyzer.ViewModel.DataTemplate;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._DataTemplate._Filter
{
    [TestFixture]
    public class InvokePropertyChanged
    {
        private bool _propertyChanged;

        [Test]
        public void Usage()
        {
            Filter filter = new Filter();
            Assert.That(filter, Is.Not.Null);

            filter.PropertyChanged += PropertyChangedEventHandle;
            Assert.That(_propertyChanged, Is.False);

            filter.DisplayTime = false;
            filter.PropertyChanged -= PropertyChangedEventHandle;

            Assert.That(_propertyChanged, Is.True);  
        }

        private void PropertyChangedEventHandle(object sender, PropertyChangedEventArgs e)
        {
            _propertyChanged = true;
        }
    }
}