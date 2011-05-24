using System;
using Finaltec.Hart.Analyzer.ViewModel.DataModels;
using Finaltec.Hart.Analyzer.ViewModel.DataTemplate;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._DataModels._DataTransferModel
{
    [TestFixture]
    public class GetInstance
    {
        [Test]
        public void Usage()
        {
            DataTransferModel dataTransferModel = DataTransferModel.GetInstance();
            Assert.That(dataTransferModel, Is.Not.Null);
            Assert.That(dataTransferModel.Output, Is.Not.Null);
            Assert.That(dataTransferModel.HartDeviceProducer, Is.Not.Null);
            Assert.That(dataTransferModel.HartDeviceProducer.Count, Is.GreaterThan(0));

            Assert.That(dataTransferModel.Output.Count, Is.EqualTo(0));  
            
            dataTransferModel.Output.Add(new CommandData(InformationType.Receive, 1, 0, "0", 0, new byte[0], 0));
            Assert.That(dataTransferModel.Output.Count, Is.EqualTo(1));
            Assert.That(dataTransferModel.Output[0].Address, Is.EqualTo("0"));  
        }
    }
}