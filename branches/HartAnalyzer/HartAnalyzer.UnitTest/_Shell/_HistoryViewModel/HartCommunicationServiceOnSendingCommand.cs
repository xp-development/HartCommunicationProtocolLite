using System;
using Communication.Hart;
using FluentAssertions;
using HartAnalyzer.Services;
using HartAnalyzer.Shell;
using HartAnalyzer.SpecificCommands;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._Shell._HistoryViewModel
{
    [TestFixture]
    public class HartCommunicationServiceOnSendingCommand
    {
        [Test]
        public void ShouldContainsAllSendingCommands()
        {
            var applicationServices = new ApplicationServices();
            var hartCommunicationService = new TestHartCommunicationService();
            applicationServices.HartCommunicationService = hartCommunicationService;

            var viewModel = new HistoryViewModel(applicationServices);

            hartCommunicationService.SimulateSendingCommand(new CommandRequest(Command.Zero()));
            hartCommunicationService.SimulateSendingCommand(new CommandRequest(Command.Zero()));

            viewModel.Items.Count.Should().Be(2);

            viewModel.Items[0].Address[0].Should().Be(128);
            viewModel.Items[0].Command[0].Should().Be(0);
            viewModel.Items[0].CommandType.Should().Be(CommandType.Send);
            viewModel.Items[0].Delimiter[0].Should().Be(2);
            viewModel.Items[0].Length[0].Should().Be(0);
            viewModel.Items[0].Preambles[2].Should().Be(0xFF);
            viewModel.Items[0].Preambles[0].Should().Be(0xFF);
            viewModel.Items[0].Preambles[1].Should().Be(0xFF);
            viewModel.Items[0].Preambles[3].Should().Be(0xFF);
            viewModel.Items[0].Preambles[4].Should().Be(0xFF);
            viewModel.Items[0].Time.Should().BeAfter(DateTime.MinValue);
        }

        [Test]
        public void ShouldContainsAllReceives()
        {
            var applicationServices = new ApplicationServices();
            var hartCommunicationService = new TestHartCommunicationService();
            applicationServices.HartCommunicationService = hartCommunicationService;

            var viewModel = new HistoryViewModel(applicationServices);

            var command = Command.Zero();
            command.ResponseCode = new byte[] {0x00, 0x01};
            hartCommunicationService.SimulateReceive(new CommandResult(command));
            hartCommunicationService.SimulateReceive(new CommandResult(command));

            viewModel.Items.Count.Should().Be(2);

            viewModel.Items[0].Address[0].Should().Be(128);
            viewModel.Items[0].Command[0].Should().Be(0);
            viewModel.Items[0].CommandType.Should().Be(CommandType.Receive);
            viewModel.Items[0].Delimiter[0].Should().Be(2);
            viewModel.Items[0].Length[0].Should().Be(0);
            viewModel.Items[0].Preambles[0].Should().Be(0xFF);
            viewModel.Items[0].Preambles[1].Should().Be(0xFF);
            viewModel.Items[0].Preambles[2].Should().Be(0xFF);
            viewModel.Items[0].Preambles[3].Should().Be(0xFF);
            viewModel.Items[0].Preambles[4].Should().Be(0xFF);
            viewModel.Items[0].Time.Should().BeAfter(DateTime.MinValue);
            viewModel.Items[0].ResponseCode[0].Should().Be(0x00);
            viewModel.Items[0].ResponseCode[1].Should().Be(0x01);
        }
    }
}