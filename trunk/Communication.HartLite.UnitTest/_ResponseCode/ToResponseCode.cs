using NUnit.Framework;

namespace Communication.HartLite.UnitTest._ResponseCode
{
    [TestFixture]
    public class ToResponseCode
    {
        [Test]
        public void Usage()
        {
            var responseCode = ResponseCode.ToResponseCode(new byte[] {5, 4});

            Assert.That(responseCode.FirstByte, Is.EqualTo(5));
            Assert.That(responseCode.SecondByte, Is.EqualTo(4));
        }

        [Test]
        public void UseIndexer()
        {
            var responseCode = ResponseCode.ToResponseCode(new byte[] {5, 4});

            Assert.That(responseCode[0], Is.EqualTo(5));
            Assert.That(responseCode[1], Is.EqualTo(4));
        }

        [Test]
        public void ShouldThrowIndexOutOfRangeExceptionIfWrongIndexerIsUsed()
        {
            var responseCode = ResponseCode.ToResponseCode(new byte[] {5, 4});

            Assert.Throws<System.IndexOutOfRangeException>(() => { byte val = responseCode[-1]; });
            Assert.Throws<System.IndexOutOfRangeException>(() => { byte val = responseCode[2]; });
        }
    }
}