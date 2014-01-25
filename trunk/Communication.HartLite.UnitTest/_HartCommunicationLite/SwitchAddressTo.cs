using System.IO;
using NUnit.Framework;

namespace Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture]
    public class SwitchAddressTo
    {
        static SwitchAddressTo()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Finaltec.Communication.HartLite.UnitTest.log4net"));
        }

        [Test]
        public void Usage()
        {
            HartCommunicationLite communication = new HartCommunicationLite("COM1");

            Assert.That(communication.Address, Is.Null);

            LongAddress newAddress = new LongAddress(22, 22, new byte[] {11, 22, 33});
            communication.SwitchAddressTo(newAddress);

            Assert.That(communication.Address.ToByteArray(), Is.EqualTo(newAddress.ToByteArray()));
        }
    }
}