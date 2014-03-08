using FluentAssertions;
using HartAnalyzer.ConnectionConfiguration;
using HartAnalyzer.Services;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._ConnectionConfiguration._ConnectionConfigurationViewModel
{
    [TestFixture]
    public class SelectedPortName
    {
        [Test]
        public void ShouldReceivePortNameFromHartCommunicationService()
        {
            var viewModel = new ConnectionConfigurationViewModel(new ApplicationServices
                {
                    HartCommunicationService = new TestHartCommunicationService("COM2")
                });

            viewModel.SelectedPortName.DataValue.Should().Be("COM2");
        }

        [Test]
        public void ShouldSetPortNameOfHartCommunicationService()
        {
            var service = new TestHartCommunicationService("COM2");

            new ConnectionConfigurationViewModel(new ApplicationServices
                {
                    HartCommunicationService = service
                })
                {
                    SelectedPortName = {DataValue = "COM3"}
                };

            service.PortName.Should().Be("COM3");
        }
    }
}