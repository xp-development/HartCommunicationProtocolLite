using FluentAssertions;
using HartAnalyzer.Services;
using Moq;
using NUnit.Framework;

namespace HartAnalyzer.Modules.Common.UnitTest._StatusBarViewModel
{
    [TestFixture]
    public class PortState
    {
        [TestCase(Services.PortState.Opened)]
        [TestCase(Services.PortState.Opening)]
        [TestCase(Services.PortState.Closed)]
        [TestCase(Services.PortState.Closing)]
        public void ShouldReturnDefaultValueOfService(Services.PortState expectedPortState)
        {
            var communicationMock = new Mock<IHartCommunicationService>();
            communicationMock.SetupGet(item => item.PortState).Returns(expectedPortState);

            var viewModel = new StatusBarViewModel(communicationMock.Object);

            viewModel.PortState.Should().Be(expectedPortState);
        }

        [Test]
        public void ShouldBeChangedIfServiceChangeTheState()
        {
            var communicationService = new TestHartCommunicationService();
            var viewModel = new StatusBarViewModel(communicationService);

            communicationService.PortState = Services.PortState.Opening;
            viewModel.PortState.Should().Be(Services.PortState.Opening);
        }
    }
}