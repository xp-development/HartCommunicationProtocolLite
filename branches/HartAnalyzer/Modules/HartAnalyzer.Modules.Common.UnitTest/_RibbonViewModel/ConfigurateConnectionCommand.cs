using FluentAssertions;
using HartAnalyzer.Services;
using NUnit.Framework;

namespace HartAnalyzer.Modules.Common.UnitTest._RibbonViewModel
{
    [TestFixture]
    public class ConfigurateConnectionCommand
    {
        [Test]
        public void ShouldOpenDialog()
        {
            var uiVisualizerService = new Cinch.TestUIVisualizerService();
            uiVisualizerService.ShowDialogResultResponders.Enqueue(() => true);
            var viewModel = new RibbonViewModel(GetTestApplicationServices(), new CommonServices{ UiVisualizerService = uiVisualizerService });

            viewModel.ConfigurateConnectionCommand.Execute(null);

            uiVisualizerService.ShowDialogResultResponders.Count.Should().Be(0);
        }

        [TestCase(Services.PortState.Closing)]
        [TestCase(Services.PortState.Opening)]
        [TestCase(Services.PortState.Opened)]
        public void CanExecuteShouldBeFalseIfPortStateIsNotClosed(Services.PortState portState)
        {
            var uiVisualizerService = new Cinch.TestUIVisualizerService();
            var viewModel = new RibbonViewModel(GetTestApplicationServices(), new CommonServices { UiVisualizerService = uiVisualizerService }) { PortState = portState };

            viewModel.ConfigurateConnectionCommand.CanExecute(null).Should().BeFalse();
        }

        [Test]
        public void CanExecuteShouldBeTrueIfPortStateIsClosed()
        {
            var uiVisualizerService = new Cinch.TestUIVisualizerService();
            var viewModel = new RibbonViewModel(GetTestApplicationServices(), new CommonServices { UiVisualizerService = uiVisualizerService }) { PortState = Services.PortState.Closed };

            viewModel.ConfigurateConnectionCommand.CanExecute(null).Should().BeTrue();
        }

        private static ApplicationServices GetTestApplicationServices()
        {
            return new ApplicationServices{HartCommunicationService = new TestHartCommunicationService()};
        }
    }
}