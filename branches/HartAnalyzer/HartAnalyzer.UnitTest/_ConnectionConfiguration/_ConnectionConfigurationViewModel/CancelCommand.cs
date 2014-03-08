using Cinch;
using FluentAssertions;
using HartAnalyzer.ConnectionConfiguration;
using HartAnalyzer.Services;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._ConnectionConfiguration._ConnectionConfigurationViewModel
{
    [TestFixture]
    public class CancelCommand
    {
        [Test]
        public void PortNameOfServiceShouldBeResettedAfterCancelCommandExecuted()
        {
            var service = new TestHartCommunicationService("COM2");
            var viewModel = new ConnectionConfigurationViewModel(new ApplicationServices
                {
                    HartCommunicationService = service
                });
            var viewAwareStatusService = new TestViewAwareStatus();
            viewModel.InitialiseViewAwareService(viewAwareStatusService);
            viewAwareStatusService.SimulateViewIsLoadedEvent();

            viewModel.SelectedPortName.DataValue = "COM3";
            viewModel.CancelCommand.Execute(null);

            service.PortName.Should().Be("COM2");
        }

        [Test]
        public void ShouldClosePopupOnCancelCommandExecuted()
        {
            var service = new TestHartCommunicationService("COM2");
            var viewModel = new ConnectionConfigurationViewModel(new ApplicationServices
                {
                    HartCommunicationService = service
                });
            var isClosedRequested = false;
            viewModel.CloseRequest += (sender, args) => isClosedRequested = true;
 
            viewModel.CancelCommand.Execute(null);

            isClosedRequested.Should().BeTrue();
        }
    }
}