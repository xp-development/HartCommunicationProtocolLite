using Communication.Hart;
using FluentAssertions;
using HartAnalyzer.Services;
using HartAnalyzer.Shell;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._Shell._RibbonViewModel
{
    [TestFixture]
    public class ConnectionCommand
    {
        [Test]
        public async void ShouldNotShowMessageIfOpenedResultIsReceived()
        {
            var messageBoxService = new TestMessageBoxService();
            var hartCommunicationService = new TestHartCommunicationService();
            hartCommunicationService.OpenAsyncResponders.Enqueue(() => OpenResult.Opened);
            var viewModel = new RibbonViewModel(new ApplicationServices{ HartCommunicationService = hartCommunicationService}, new CommonServices{ MessageBoxService = messageBoxService });

            await viewModel.ConnectionCommand.Execute(null);

            messageBoxService.ShowInformationRequests.Count.Should().Be(0);
        }

        [TestCase(OpenResult.ComPortIsOpenAlreadyOpen)]
        [TestCase(OpenResult.ComPortNotExisting)]
        [TestCase(OpenResult.UnknownComPortError)]
        public async void ShouldShowMessageIfNotOpenedIsReceived(OpenResult openResult)
        {
            var messageBoxService = new TestMessageBoxService();
            var hartCommunicationService = new TestHartCommunicationService();
            hartCommunicationService.OpenAsyncResponders.Enqueue(() => openResult);
            var viewModel = new RibbonViewModel(new ApplicationServices { HartCommunicationService = hartCommunicationService }, new CommonServices { MessageBoxService = messageBoxService });

            await viewModel.ConnectionCommand.Execute(null);

            messageBoxService.ShowInformationRequests.Count.Should().Be(1);
            messageBoxService.ShowInformationRequests[0].Message.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async void ShouldCallOpenIfStateIsClosed()
        {
            var messageBoxService = new TestMessageBoxService();
            var hartCommunicationService = new TestHartCommunicationService { PortState = Services.PortState.Closed };
            hartCommunicationService.OpenAsyncResponders.Enqueue(() => OpenResult.Opened);
            var viewModel = new RibbonViewModel(new ApplicationServices { HartCommunicationService = hartCommunicationService }, new CommonServices { MessageBoxService = messageBoxService });

            await viewModel.ConnectionCommand.Execute(null);

            hartCommunicationService.OpenAsyncResponders.Count.Should().Be(0);
        }

        [Test]
        public async void ShouldCallCloseIfStateIsOpened()
        {
            var messageBoxService = new TestMessageBoxService();
            var hartCommunicationService = new TestHartCommunicationService { PortState = Services.PortState.Opened };
            hartCommunicationService.CloseAsyncResponders.Enqueue(() => CloseResult.Closed);
            var viewModel = new RibbonViewModel(new ApplicationServices { HartCommunicationService = hartCommunicationService }, new CommonServices { MessageBoxService = messageBoxService });

            await viewModel.ConnectionCommand.Execute(null);

            hartCommunicationService.OpenAsyncResponders.Count.Should().Be(0);
        }

        [TestCase(Services.PortState.Opened)]
        [TestCase(Services.PortState.Closed)]
        public void ShouldCanExecuteIfStateIsNotChanging(Services.PortState portState)
        {
            var messageBoxService = new TestMessageBoxService();
            var hartCommunicationService = new TestHartCommunicationService { PortState = portState };
            var viewModel = new RibbonViewModel(new ApplicationServices { HartCommunicationService = hartCommunicationService }, new CommonServices { MessageBoxService = messageBoxService });

            viewModel.ConnectionCommand.CanExecute(null).Should().BeTrue();
        }

        [TestCase(Services.PortState.Opening)]
        [TestCase(Services.PortState.Closing)]
        public void ShouldCannotExecuteIfStateIsChanging(Services.PortState portState)
        {
            var messageBoxService = new TestMessageBoxService();
            var hartCommunicationService = new TestHartCommunicationService { PortState = portState };
            var viewModel = new RibbonViewModel(new ApplicationServices { HartCommunicationService = hartCommunicationService }, new CommonServices { MessageBoxService = messageBoxService });

            viewModel.ConnectionCommand.CanExecute(null).Should().BeFalse();
        }
    }
}