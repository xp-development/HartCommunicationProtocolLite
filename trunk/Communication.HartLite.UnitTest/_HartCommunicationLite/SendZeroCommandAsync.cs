using System;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture, Explicit, Category("Manual")]
    public class SendZeroCommandAsync
    {
        static SendZeroCommandAsync()
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
            CommandResult commandResult = null;
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            communication.Receive += (sender, args) => 
                                         {
                                             commandResult = args;
                                             resetEvent.Set();
                                         };

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            communication.SendZeroCommandAsync();
            Assert.That(resetEvent.WaitOne(TimeSpan.FromSeconds(2)), Is.True);

            Assert.That(commandResult, Is.Not.Null);
            Assert.That(commandResult.CommandNumber, Is.EqualTo(0));
            Assert.That(commandResult.ResponseCode.FirstByte, Is.EqualTo(0));

            communication.Close();
        }
    }
}