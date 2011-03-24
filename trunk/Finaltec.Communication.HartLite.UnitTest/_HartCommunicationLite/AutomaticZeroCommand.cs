using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture]
    public class AutomaticZeroCommand
    {
        [Test]
        public void Usage()
        {
            HartCommunicationLite communication = new HartCommunicationLite("COM1");

            Assert.That(communication.AutomaticZeroCommand, Is.True);
        }
    }
}