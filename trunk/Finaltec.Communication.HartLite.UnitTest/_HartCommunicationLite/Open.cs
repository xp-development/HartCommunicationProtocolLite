using System.IO;
using System.IO.Ports;
using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture, Category("Manual")]
    public class Open
    {
        static Open()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Finaltec.Communication.HartLite.UnitTest.log4net"));
        }

        [Test]
        public void Usage()
        {
            const string PORT_NAME = "COM1";

            HartCommunicationLite communication = new HartCommunicationLite(PORT_NAME);
            
            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            communication.Close();
        }

        [Test]
        public void ConnectResponseResultShouldBeComPortNotExisting()
        {
            HartCommunicationLite communication = new HartCommunicationLite("notExisting");

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.ComPortNotExisting));
        }

        [Test]
        public void ConnectResponseResultShouldBeComPortIsAlreadyOpened()
        {
            const string PORT_NAME = "COM1";

            SerialPort serialPort = new SerialPort(PORT_NAME);
            serialPort.Open();

            HartCommunicationLite communication = new HartCommunicationLite(PORT_NAME);

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.ComPortIsOpenAlreadyOpen));

            serialPort.Close();
        }
    }
}