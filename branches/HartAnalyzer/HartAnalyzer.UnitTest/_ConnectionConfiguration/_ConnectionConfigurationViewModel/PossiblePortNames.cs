using FluentAssertions;
using HartAnalyzer.ConnectionConfiguration;
using HartAnalyzer.Services;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._ConnectionConfiguration._ConnectionConfigurationViewModel
{
    [TestFixture]
    public class PossiblePortNames
    {
        [Test]
        public void ShouldReceivePortNamesFromHartCommunicationService()
        {
            var viewModel = new ConnectionConfigurationViewModel(new ApplicationServices
                {
                    HartCommunicationService = new TestHartCommunicationService()
                });

            viewModel.PossiblePortNames.Count.Should().Be(3);
            viewModel.PossiblePortNames[0].Should().Be("COM1");
            viewModel.PossiblePortNames[1].Should().Be("COM2");
            viewModel.PossiblePortNames[2].Should().Be("COM3");
        }
    }
}