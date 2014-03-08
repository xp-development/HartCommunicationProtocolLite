using Cinch;
using FluentAssertions;
using HartAnalyzer.ConnectionConfiguration;
using HartAnalyzer.Services;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._ConnectionConfiguration._ConnectionConfigurationViewModel
{
    [TestFixture]
    public class SaveCommand
    {
        [Test]
        public void PortNameOfServiceShouldBeSetAfterSaveCommandExecuted()
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
            viewModel.SaveCommand.Execute(null);

            service.PortName.Should().Be("COM3");
        }

        [Test]
        public void ShouldClosePopupOnSaveCommandExecuted()
        {
            var service = new TestHartCommunicationService("COM2");
            var viewModel = new ConnectionConfigurationViewModel(new ApplicationServices
                {
                    HartCommunicationService = service
                });
            var isClosedRequested = false;
            viewModel.CloseRequest += (sender, args) => isClosedRequested = true;
 
            viewModel.SaveCommand.Execute(null);

            isClosedRequested.Should().BeTrue();
        }
    }
}