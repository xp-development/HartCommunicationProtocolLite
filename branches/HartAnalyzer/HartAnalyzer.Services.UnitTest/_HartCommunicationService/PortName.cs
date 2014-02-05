using Communication.Hart;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace HartAnalyzer.Services.UnitTest._HartCommunicationService
{
    [TestFixture]
    public class PortName
    {
        [Test]
        public void ShouldBeEqualToHartCommunicationPortName()
        {
            var communication = new Mock<IHartCommunication>();
            var service = new HartCommunicationService(communication.Object);

            communication.SetupGet(item => item.PortName).Returns("COM2");
            service.PortName.Should().Be("COM2");

            communication.SetupGet(item => item.PortName).Returns("COM3");
            service.PortName.Should().Be("COM3");
        }
    }
}