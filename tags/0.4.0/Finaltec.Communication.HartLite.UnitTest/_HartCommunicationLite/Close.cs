using System.IO;
using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture, Category("Manual")]
    public class Close
    {
        static Close()
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

            CloseResult closeResult = communication.Close();
            Assert.That(closeResult, Is.EqualTo(CloseResult.Closed));
        }
    }
}