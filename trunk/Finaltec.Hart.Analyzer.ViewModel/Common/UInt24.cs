using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// UInt24 struct.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true)]
    public struct UInt24 : IComparable, IFormattable, IConvertible, IComparable<UInt24>, IEquatable<UInt24>
    {
        private uint _value;

        // ReSharper disable InconsistentNaming
        /// <summary>
        /// UInt24 signed type max value.
        /// </summary>
        public const uint MaxValue = 16777215;
        /// <summary>
        /// UInt24 signed type min value.
        /// </summary>
        public const uint MinValue = 0;
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="Value"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\Value.cs" lang="CSharp" />
        /// </example>
        public uint Value
        {
            get { return _value; }
            set
            {
                if (value > MaxValue || value < MinValue)
                    throw new OverflowException("Value was either too large or too small for an UInt24.");

                _value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UInt24"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="UInt24"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\Ctor.cs" lang="CSharp" />
        /// </example>
        public UInt24(uint value = 0)
            : this()
        {
            if (value > MaxValue)
                value = value - 16777216;

            if (value > MaxValue || value < MinValue)
                throw new OverflowException("Value was either too large or too small for an UInt24.");

            Value = value;
        }

        /// <summary>
        /// Tries to parse the string to a UInt24.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of TryParse:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\TryParse.cs" lang="CSharp" />
        /// </example>
        public static bool TryParse(string s, out UInt24 result)
        {
            return TryParse(s, NumberStyles.Integer, null, out result);
        }

        /// <summary>
        /// Tries to parse the string to a UInt24.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="style">The style.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of TryParse:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\TryParse.cs" lang="CSharp" />
        /// </example>
        public static bool TryParse(string s, NumberStyles style, IFormatProvider formatProvider, out UInt24 result)
        {
            uint output;
            bool parse = UInt32.TryParse(s, style, formatProvider, out output);

            if (!parse)
            {
                result = new UInt24();
                return false;
            }

            if (output > MaxValue || output < MinValue)
            {
                result = new UInt24();
                return false;
            }

            result = new UInt24(output);
            return true;
        }

        /// <summary>
        /// Converts to a three byte array.
        /// </summary>
        /// <returns>
        /// A three byte long array.
        /// </returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ToByteArray"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ToByteArray.cs" lang="CSharp" />
        /// </example>
        public byte[] ToByteArray()
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            byte[] returnByteArr = new byte[3];

            for (uint i = 0; i < returnByteArr.Length; i++)
            {
                returnByteArr[i] = bytes[i];
            }

            return returnByteArr;
        }

        /// <summary>
        /// Froms the byte array.
        /// </summary>
        /// <param name="dataBytes">The data bytes.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="FromByteArray"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\FromByteArray.cs" lang="CSharp" />
        /// </example>
        public static UInt24 FromByteArray(byte[] dataBytes)
        {
            if (dataBytes == null || dataBytes.Length != 3)
                throw new ArgumentException("Invalid data byte length for a UInt24!", "dataBytes");

            return new UInt24(BitConverter.ToUInt32(new byte[]
                                                        {
                                                            dataBytes[0], dataBytes[1], dataBytes[2], 0
                                                        }, 0));
        }

        #region Implementation of IComparable

        /// <summary>
        /// Compares to the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }

            if (!(value is UInt24))
            {
                throw new ArgumentException("Value is not from type UInt24.", "value");
            }

            UInt24 num = (UInt24)value;
            if (Value < num.Value)
            {
                return -1;
            }
            if (Value > num.Value)
            {
                return 1;
            }

            return 0;
        }

        #endregion

        #region Implementation of IFormattable

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString(format, formatProvider);
        }

        #endregion

        #region Implementation of IConvertible

        /// <summary>
        /// Gets thr <see cref="T:System.TypeCode"/> for this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.TypeCode"/> for this instance.
        /// </returns>
        public TypeCode GetTypeCode()
        {
            throw new NotSupportedException("No typecode exists for a UInt24 value.");
        }

        /// <summary>
        /// Converts the value to a boolean.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Value);
        }

        /// <summary>
        /// Converts the value to a char.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }

        /// <summary>
        /// Converts the value to a sbyte.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        /// <summary>
        /// Converts the value to a byte.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        /// <summary>
        /// Converts the value to a short.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        /// <summary>
        /// Converts the value to a ushort.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value);
        }

        /// <summary>
        /// Converts the value to a uint.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }

        /// <summary>
        /// Converts the value to a uuint.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value);
        }

        /// <summary>
        /// Converts the value to a long.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }

        /// <summary>
        /// Converts the value to a ulong.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value);
        }

        /// <summary>
        /// Converts the value to a float.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        /// <summary>
        /// Converts the value to a double.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        /// <summary>
        /// Converts the value to a decimal.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        /// <summary>
        /// Converts the value to a DateTime.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new NotSupportedException("The ToDateTime convertation is not supportet on a UInt24 value.");
        }

        /// <summary>
        /// Converts the value to a string.
        /// </summary>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>
        /// A convertet value.
        /// </returns>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return Value.ToString(provider);
        }

        /// <summary>
        /// Converts the value to a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="provider">A implementation of <see cref="T:System.IFormatProvider"/> for additional format informations.</param>
        /// <returns>A convertet value.</returns>
        object IConvertible.ToType(Type type, IFormatProvider provider)
        {
            throw new NotSupportedException("The ToType convertation is not supportet on a UInt24 value.");
        }

        #endregion

        #region Implementation of IComparable<UInt24>

        /// <summary>
        /// Compares to the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public int CompareTo(UInt24 value)
        {
            if (Value < value.Value)
            {
                return -1;
            }

            if (Value > value.Value)
            {
                return 1;
            }

            return 0;
        }

        #endregion

        #region Implementation of IEquatable<UInt24>

        /// <summary>
        /// Equalses the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public bool Equals(UInt24 obj)
        {
            return Value.Equals(obj.Value);
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return ((obj is UInt24) && (Value == ((UInt24)obj).Value)) || ((obj is uint) && (Value == (uint)obj));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #region OperatorOverloading

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.UInt32"/> to <see cref="UInt24"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of = operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static implicit operator UInt24(uint value)
        {
            return new UInt24(value);
        }

        /// <summary>
        /// Implements the operator ~.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of ~ operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator ~(UInt24 value)
        {
            byte[] byteArray = value.ToByteArray();
            return new UInt24(BitConverter.ToUInt32(new byte[]
                                                        {
                                                            (byte)(~byteArray[0]), (byte)(~byteArray[1]), (byte)(~byteArray[2]), 0 
                                                        }, 0));
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of + operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator +(UInt24 value1, UInt24 value2)
        {
            return new UInt24(value1.Value + value2.Value);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of - operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator -(UInt24 value1, UInt24 value2)
        {
            return new UInt24(value1.Value - value2.Value);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of * operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator *(UInt24 value1, UInt24 value2)
        {
            return new UInt24(value1.Value * value2.Value);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of / operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator /(UInt24 value1, UInt24 value2)
        {
            return new UInt24(value1.Value / value2.Value);
        }

        /// <summary>
        /// Implements the operator %.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of % operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator %(UInt24 value1, UInt24 value2)
        {
            return new UInt24(value1.Value % value2.Value);
        }

        /// <summary>
        /// Implements the operator ++.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of ++ operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator ++(UInt24 value)
        {
            value += 1;
            return value;
        }

        /// <summary>
        /// Implements the operator --.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of -- operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator --(UInt24 value)
        {
            value -= 1;
            return value;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of == operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static bool operator ==(UInt24 value1, UInt24 value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of != operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static bool operator !=(UInt24 value1, UInt24 value2)
        {
            return value1.Value != value2.Value;
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of &lt; operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static bool operator <(UInt24 value1, UInt24 value2)
        {
            return value1.Value < value2.Value;
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of &gt; operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static bool operator >(UInt24 value1, UInt24 value2)
        {
            return value1.Value > value2.Value;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of &lt;= operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static bool operator <=(UInt24 value1, UInt24 value2)
        {
            return value1.Value <= value2.Value;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of &gt;= operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static bool operator >=(UInt24 value1, UInt24 value2)
        {
            return value1.Value >= value2.Value;
        }

        /// <summary>
        /// Implements the operator &amp;.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of and operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator &(UInt24 value1, UInt24 value2)
        {
            return new UInt24(value1.Value & value2.Value);
        }

        /// <summary>
        /// Implements the operator |.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of | operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator |(UInt24 value1, UInt24 value2)
        {
            return new UInt24(value1.Value | value2.Value);
        }

        /// <summary>
        /// Implements the operator ^.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of ^ operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator ^(UInt24 value1, UInt24 value2)
        {
            return new UInt24(value1.Value ^ value2.Value);
        }

        /// <summary>
        /// Implements the operator !.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of ! operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator !(UInt24 value)
        {
            return ~value;
        }

        /// <summary>
        /// Implements the operator &lt;&lt;.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="move">The move.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of &lt;&lt; operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator <<(UInt24 value, int move)
        {
            uint val = value.Value << move;

            if (val > MaxValue || val < MinValue)
                return new UInt24();

            return new UInt24(val);
        }

        /// <summary>
        /// Implements the operator &gt;&gt;.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="move">The move.</param>
        /// <returns>The result of the operator.</returns>
        /// <example>
        /// With the following UnitTest we test the functionality of &gt;&gt; operator:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\OperatorTests.cs" lang="CSharp" />
        /// </example>
        public static UInt24 operator >>(UInt24 value, int move)
        {
            uint val = value.Value >> move;

            if (val > MaxValue || val < MinValue)
                return new UInt24();

            return new UInt24(val);
        }

        #endregion

        #region Implementation of ConvertableFromDefaultTypes

        /// <summary>
        /// Converts from byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromByte"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromByte(byte value)
        {
            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from S byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromSByte"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromSByte(sbyte value)
        {
            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromShort"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromShort(short value)
        {
            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from U short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromUShort"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromUShort(ushort value)
        {
            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from uint.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromInt"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromInt(int value)
        {
            if (value > MaxValue || value < MinValue)
                throw new OverflowException("Value was either too large or too small for an UInt24.");

            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from U uint.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromUInt"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromUInt(uint value)
        {
            if (value > MaxValue || value < MinValue)
                throw new OverflowException("Value was either too large or too small for an UInt24.");

            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from long.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromLong"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromLong(long value)
        {
            if (value > MaxValue || value < MinValue)
                throw new OverflowException("Value was either too large or too small for an UInt24.");

            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from U long.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromULong"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromULong(ulong value)
        {
            if (value > MaxValue)
                throw new OverflowException("Value was either too large or too small for an UInt24.");

            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from float.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromFloat"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromFloat(float value)
        {
            if (value > MaxValue || value < MinValue)
                throw new OverflowException("Value was either too large or too small for an UInt24.");

            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromDouble"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromDouble(double value)
        {
            if (value > MaxValue || value < MinValue)
                throw new OverflowException("Value was either too large or too small for an UInt24.");

            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromDecimal"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromDecimal(decimal value)
        {
            if (value > MaxValue || value < MinValue)
                throw new OverflowException("Value was either too large or too small for an UInt24.");

            uint val = Convert.ToUInt32(value);

            if (val > MaxValue)
                val = val & MaxValue;

            if (val < MinValue)
                val = val & MinValue;

            return new UInt24(val);
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <example>
        /// With the following UnitTest we test the functionality of <see cref="ConvertFromString"/>:
        /// <code source="..\CoreFrame.UnitTests\_UInt24\ConvertingFromDefaultTypes.cs" lang="CSharp" />
        /// </example>
        public static UInt24 ConvertFromString(string value)
        {
            UInt24 val;
            TryParse(value, out val);

            return val;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}