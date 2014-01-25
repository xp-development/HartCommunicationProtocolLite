using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture, Explicit, Category("Manual")]
    public class SendAsync
    {
        static SendAsync()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Finaltec.Communication.HartLite.UnitTest.log4net"));
        }

        [Test]
        public void Usage()
        {
            HartCommunicationLite communication = new HartCommunicationLite("COM1");
            List<CommandResult> commandResults = new List<CommandResult>();
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            communication.Receive += (sender, args) => 
                                         {
                                             commandResults.Add(args);
                                             if(args.CommandNumber == 12)
                                                resetEvent.Set();
                                         };

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            communication.SendAsync(12);
            Assert.That(resetEvent.WaitOne(TimeSpan.FromSeconds(20)), Is.True);

            Assert.That(commandResults.Count, Is.EqualTo(2));
            Assert.That(commandResults[0].CommandNumber, Is.EqualTo(0));
            Assert.That(commandResults[0].ResponseCode.FirstByte, Is.EqualTo(0));
            Assert.That(commandResults[1].CommandNumber, Is.EqualTo(12));
            Assert.That(commandResults[1].ResponseCode.FirstByte, Is.EqualTo(0));

            communication.Close();
        }

        [Test]
        public void FirstSendZeroCommandSynchronSecondSendCommand12Asynchron()
        {
            HartCommunicationLite communication = new HartCommunicationLite("COM1");
            List<CommandResult> commandResults = new List<CommandResult>();
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            communication.Receive += (sender, args) => 
                                         {
                                             commandResults.Add(args);
                                             if(args.CommandNumber == 13)
                                                resetEvent.Set();
                                         };

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            CommandResult zeroCommand = communication.SendZeroCommand();

            for (int i = 0; i < 10; i++)
            {
                communication.SendAsync(12);
            }
            communication.SendAsync(13);
            Assert.That(resetEvent.WaitOne(TimeSpan.FromSeconds(100)), Is.True);

            Assert.That(commandResults.Count, Is.EqualTo(12));
            Assert.That(zeroCommand.CommandNumber, Is.EqualTo(0));
            Assert.That(zeroCommand.ResponseCode.FirstByte, Is.EqualTo(0));
            Assert.That(commandResults[1].CommandNumber, Is.EqualTo(12));
            Assert.That(commandResults[1].ResponseCode.FirstByte, Is.EqualTo(0));

            communication.Close();
        }
    }
}