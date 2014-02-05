using Communication.Hart;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace HartAnalyzer.Services.UnitTest._HartCommunicationService
{
    [TestFixture]
    public class OpenAsync
    {
        [Test]
        public async void ShouldOpenPort()
        {
            var mock = new Mock<IHartCommunication>();
            mock.Setup(item => item.Open()).Returns(OpenResult.Opened);
            var service = new HartCommunicationService(mock.Object);

            var openResult = await service.OpenAsync();

            openResult.Should().Be(OpenResult.Opened);
        }

        [Test]
        public async void ShouldOpenPortOnlyOneTimesIfPortIsAlreadyOpen()
        {
            var mock = new Mock<IHartCommunication>();
            mock.Setup(item => item.Open()).Returns(OpenResult.Opened);
            var service = new HartCommunicationService(mock.Object);

            var openResult = await service.OpenAsync();
            openResult.Should().Be(OpenResult.Opened);

            await service.OpenAsync();

            mock.Verify(item => item.Open(), Times.Exactly(1));
        }

        [TestCase(OpenResult.ComPortIsOpenAlreadyOpen)]
        [TestCase(OpenResult.ComPortNotExisting)]
        [TestCase(OpenResult.UnknownComPortError)]
        public async void ShouldReturnOpenResultErrorIfPortCannotOpened(OpenResult openResult)
        {
            var mock = new Mock<IHartCommunication>();
            mock.Setup(item => item.Open()).Returns(openResult);
            var service = new HartCommunicationService(mock.Object);

            (await service.OpenAsync()).Should().Be(openResult);
        }
    }
}