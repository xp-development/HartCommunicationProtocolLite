using System;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture, Explicit, Category("Manual")]
    public class Receive
    {
        static Receive()
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
            CommandResult receivedCommandResult = null;
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            communication.Receive += (sender, args) => 
                                                    {
                                                        receivedCommandResult = args;
                                                        resetEvent.Set();
                                                    };

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            CommandResult commandResult = communication.SendZeroCommand();
            Assert.That(commandResult, Is.Not.Null);
            Assert.That(commandResult.CommandNumber, Is.EqualTo(0));
            Assert.That(commandResult.ResponseCode.FirstByte, Is.EqualTo(0));

            Assert.That(resetEvent.WaitOne(TimeSpan.FromSeconds(2)), Is.True);
            Assert.That(commandResult.Address, Is.EqualTo(receivedCommandResult.Address));
            Assert.That(commandResult.CommandNumber, Is.EqualTo(receivedCommandResult.CommandNumber));
            Assert.That(commandResult.PreambleLength, Is.EqualTo(receivedCommandResult.PreambleLength));
            Assert.That(commandResult.ResponseCode.FirstByte, Is.EqualTo(receivedCommandResult.ResponseCode.FirstByte));
            Assert.That(commandResult.ResponseCode.SecondByte, Is.EqualTo(receivedCommandResult.ResponseCode.SecondByte));

            communication.Close();
        }
    }
}