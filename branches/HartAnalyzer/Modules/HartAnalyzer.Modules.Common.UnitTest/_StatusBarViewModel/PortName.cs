using FluentAssertions;
using HartAnalyzer.Services;
using NUnit.Framework;

namespace HartAnalyzer.Modules.Common.UnitTest._StatusBarViewModel
{
    [TestFixture]
    public class PortName
    {
        [Test]
        public void ShouldBeSetIfStateIsOpened()
        {
            var communicationService = new TestHartCommunicationService {PortState = Services.PortState.Opened};
            var viewModel = new StatusBarViewModel(communicationService);

            viewModel.PortName.Should().Be("COM1");
        }

        [TestCase(Services.PortState.Closed)]
        [TestCase(Services.PortState.Closing)]
        [TestCase(Services.PortState.Opening)]
        public void ShouldBeSetTooIfStateIsNotOpened(Services.PortState portState)
        {
            var communicationService = new TestHartCommunicationService { PortState = portState };
            var viewModel = new StatusBarViewModel(communicationService);

            viewModel.PortName.Should().Be("COM1");
        }
    }
}