using System;
using Finaltec.Hart.Analyzer.ViewModel.DataTemplate;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._DataTemplate._Filter
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            Filter filter = new Filter();
            Assert.That(filter, Is.Not.Null);

            Assert.That(filter.DisplayAddress, Is.Not.Null);
            Assert.That(filter.DisplayByteCount, Is.Not.Null);
            Assert.That(filter.DisplayChecksum, Is.Not.Null);
            Assert.That(filter.DisplayCommand, Is.Not.Null);
            Assert.That(filter.DisplayData, Is.Not.Null);
            Assert.That(filter.DisplayDelimiter, Is.Not.Null);
            Assert.That(filter.DisplayPreamble, Is.Not.Null);
            Assert.That(filter.DisplayResponse, Is.Not.Null);
            Assert.That(filter.DisplaySendOrRecived, Is.Not.Null);
            Assert.That(filter.DisplayTime, Is.Not.Null);
        }
    }
}