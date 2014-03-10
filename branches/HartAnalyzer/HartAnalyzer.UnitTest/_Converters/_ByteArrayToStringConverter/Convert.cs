using System;
using System.Globalization;
using System.Windows.Data;
using FluentAssertions;
using HartAnalyzer.Converters;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._Converters._ByteArrayToStringConverter
{
    [TestFixture]
    public class Convert
    {
        [Test]
        public void Usage()
        {
            var converter = new ByteArrayToStringConverter();

            var result = converter.Convert(new byte[] {3, 5, 2, 77}, typeof (string), null, CultureInfo.CurrentCulture);

            result.Should().Be("03-05-02-4D");
        }

        [Test]
        public void ShouldReturnBindingDoNothingIfValueIsNull()
        {
            var converter = new ByteArrayToStringConverter();

            var result = converter.Convert(null, typeof (string), null, CultureInfo.CurrentCulture);

            result.Should().Be(Binding.DoNothing);
        }

        [Test]
        public void ShouldThrowExceptionIfValueIfNotByteArray()
        {
            var converter = new ByteArrayToStringConverter();

            Assert.Throws<NotSupportedException>(() => converter.Convert(3333, typeof (string), null, CultureInfo.CurrentCulture));
        }
    }
}