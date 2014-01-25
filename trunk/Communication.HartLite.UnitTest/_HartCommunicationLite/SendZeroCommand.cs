using System.IO;
using NUnit.Framework;

namespace Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture, Explicit, Category("Manual")]
    public class SendZeroCommand
    {
        static SendZeroCommand()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Finaltec.Communication.HartLite.UnitTest.log4net"));
        }

        [Test]
        public void Usage()
        {
            HartCommunicationLite communication = new HartCommunicationLite("COM1")
                                                      {
                                                          AutomaticZeroCommand = false
                                                      };

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            CommandResult command = communication.SendZeroCommand();

            Assert.That(command, Is.Not.Null);
            Assert.That(command.CommandNumber, Is.EqualTo(0));
            Assert.That(command.ResponseCode.FirstByte, Is.EqualTo(0));

            communication.Close();
        }
    }
}