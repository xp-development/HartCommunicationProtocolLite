using System;
using System.Globalization;
using System.Threading;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._Common._TypeParser
{
    [TestFixture]
    public class TryParse
    {
        [Test]
        public void ByteUsage()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("(byte)120");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(120));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(1));  
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(120));

            parseReturnValue = TypeParser.TryParse("20");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(20));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(1));  
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(20));  
        }

        [Test]
        public void ShortUsage()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("(short)32647");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(135));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(127));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(2));  
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(32647));

            parseReturnValue = TypeParser.TryParse("(int16)32647");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(135));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(127));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(2));  
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(32647));  
        }

        [Test]
        public void UnsignedShortUsage()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("(ushort)32647");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(135));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(127));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(2));  
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(32647));

            parseReturnValue = TypeParser.TryParse("(uint16)32647");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(135));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(127));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(2));  
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(32647));  
        }

        [Test]
        public void Int24Usage()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("(int24)-1677721");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(103));  
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(102));
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(230));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(3));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(-1677721));  
        }

        [Test]
        public void UnsignedInt24Usage()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("(uint24)10066329");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(153));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(153));
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(153));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(3));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(10066329));  
        }

        [Test]
        public void Int32Usage()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("(int)1644455577");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(153));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(102));
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(4));
            Assert.That(parseReturnValue.ByteArray[3], Is.EqualTo(98));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(4));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(1644455577));

            parseReturnValue = TypeParser.TryParse("(int32)1644455577");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(153));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(102));
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(4));
            Assert.That(parseReturnValue.ByteArray[3], Is.EqualTo(98));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(4));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(1644455577));  
        }

        [Test]
        public void UnsignedInt32Usage()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("(uint)1644455577");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(153));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(102));
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(4));
            Assert.That(parseReturnValue.ByteArray[3], Is.EqualTo(98));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(4));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(1644455577));

            parseReturnValue = TypeParser.TryParse("(uint32)1644455577");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(153));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(102));
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(4));
            Assert.That(parseReturnValue.ByteArray[3], Is.EqualTo(98));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(4));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(1644455577));
        }

        [Test]
        public void FloatUsage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            ParseReturnValue parseReturnValue = TypeParser.TryParse("(float)3,4");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(154));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(153));
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(89));
            Assert.That(parseReturnValue.ByteArray[3], Is.EqualTo(64));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(4));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(3.4f));
        }

        [Test]
        public void PackedAsciiUsage()
        {
            NotImplementedException exception = Assert.Throws<NotImplementedException>(() => TypeParser.TryParse("'Hello World'"));

            Assert.That(exception.Message, Is.EqualTo("Currently not implementet. Value was 'Hello World'."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(NotImplementedException)));

            exception = Assert.Throws<NotImplementedException>(() => TypeParser.TryParse("(packed)Hello World"));

            Assert.That(exception.Message, Is.EqualTo("Currently not implementet. Value was Hello World."));
            Assert.That(exception.GetType(), Is.EqualTo(typeof(NotImplementedException)));  
        }

        [Test]
        public void StringUsage()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("\"Hello World\"");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(72));  
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(101));  
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(108));  
            Assert.That(parseReturnValue.ByteArray[3], Is.EqualTo(108));  
            Assert.That(parseReturnValue.ByteArray[4], Is.EqualTo(111));  
            Assert.That(parseReturnValue.ByteArray[5], Is.EqualTo(32));  
            Assert.That(parseReturnValue.ByteArray[6], Is.EqualTo(87));  
            Assert.That(parseReturnValue.ByteArray[7], Is.EqualTo(111));  
            Assert.That(parseReturnValue.ByteArray[8], Is.EqualTo(114));  
            Assert.That(parseReturnValue.ByteArray[9], Is.EqualTo(108));  
            Assert.That(parseReturnValue.ByteArray[10], Is.EqualTo(100));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(11));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo("Hello World"));

            parseReturnValue = TypeParser.TryParse("(string)Hello World");

            Assert.That(parseReturnValue.ByteArray[0], Is.EqualTo(72));
            Assert.That(parseReturnValue.ByteArray[1], Is.EqualTo(101));
            Assert.That(parseReturnValue.ByteArray[2], Is.EqualTo(108));
            Assert.That(parseReturnValue.ByteArray[3], Is.EqualTo(108));
            Assert.That(parseReturnValue.ByteArray[4], Is.EqualTo(111));
            Assert.That(parseReturnValue.ByteArray[5], Is.EqualTo(32));
            Assert.That(parseReturnValue.ByteArray[6], Is.EqualTo(87));
            Assert.That(parseReturnValue.ByteArray[7], Is.EqualTo(111));
            Assert.That(parseReturnValue.ByteArray[8], Is.EqualTo(114));
            Assert.That(parseReturnValue.ByteArray[9], Is.EqualTo(108));
            Assert.That(parseReturnValue.ByteArray[10], Is.EqualTo(100));
            Assert.That(parseReturnValue.ByteArray.Length, Is.EqualTo(11));
            Assert.That(parseReturnValue.ErrorMessage, Is.Null);
            Assert.That(parseReturnValue.ParseResult, Is.True);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo("Hello World"));  
        }

        [Test]
        public void ByteFail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("256", typeof(byte));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value '256' is too large for an Byte."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0));  
        }

        [Test]
        public void ShortFail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("-32769", typeof(short));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value '-32769' is too small for an Int16."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0));  
        }

        [Test]
        public void UnsignedShortFail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("WILL FAIL", typeof(ushort));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value 'WILL FAIL' was not in a correct format for an UInt16."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0));  
        }

        [Test]
        public void Int24Fail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("8388608", typeof(Int24));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value '8388608' is too large for an Int24."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0)); 
        }

        [Test]
        public void UnsignedInt24Fail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("-1", typeof(UInt24));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value '-1' was not in a correct format for an UInt24."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0));
        }

        [Test]
        public void Int32Fail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("-2147483649", typeof(int));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value '-2147483649' is too small for an Int32."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0));  
        }

        [Test]
        public void UnsignedInt32Fail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("-1", typeof(uint));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value '-1' was not in a correct format for an UInt32."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0));
        }

        [Test]
        public void FloatWillFail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("35,1E+38", typeof(float));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value '35,1E+38' is too large for an Float."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0));

            parseReturnValue = TypeParser.TryParse("-35,1E+38", typeof(float));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value '-35,1E+38' is too small for an Float."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0));

            parseReturnValue = TypeParser.TryParse("FAIL", typeof(float));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("Value 'FAIL' was not in a correct format for an Float."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(0)); 
        }

        [Test]
        public void PackedAsciiFail()
        {
            //todo implement PackedAscii UnitTests if packed ascii exists.
        }

        [Test]
        public void StringFail()
        {
            ParseReturnValue parseReturnValue = TypeParser.TryParse("\"Fail Test", typeof (string));

            Assert.That(parseReturnValue.ByteArray, Is.Null);
            Assert.That(parseReturnValue.ErrorMessage, Is.EqualTo("\" missing on end of string 'Fail Test'."));
            Assert.That(parseReturnValue.ParseResult, Is.False);
            Assert.That(parseReturnValue.ParseValue, Is.EqualTo(null));
        }
    }
}