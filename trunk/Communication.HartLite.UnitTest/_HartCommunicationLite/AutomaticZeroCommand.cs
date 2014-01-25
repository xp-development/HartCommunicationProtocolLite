using NUnit.Framework;

namespace Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture]
    public class AutomaticZeroCommand
    {
        [Test]
        public void Usage()
        {
            var communication = new HartCommunicationLite("COM1");

            Assert.That(communication.AutomaticZeroCommand, Is.True);
        }
    }
}