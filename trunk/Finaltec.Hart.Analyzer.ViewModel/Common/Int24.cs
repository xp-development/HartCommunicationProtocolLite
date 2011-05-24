using System;
using System.Globalization;
using System.Linq;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// Int24 struct.
    /// </summary>
    public struct Int24
    {
        private int _value;

        /// <summary>
        /// Int24 signed type max value.
        /// </summary>
        public const int MAX_VALUE = 8388607;
        /// <summary>
        /// Int24 signed type min value.
        /// </summary>
        public const int MIN_VALUE = -8388608;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get { return _value; }
            set
            {
                if (value > MAX_VALUE || value < MIN_VALUE)
                    throw new ArgumentException("Value was either too large or too small for an Int24.");

                _value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int24"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Int24(int value = 0) : this()
        {
            if (value > MAX_VALUE)
                value = value - 16777216;

            if (value > MAX_VALUE || value < MIN_VALUE)
                throw new ArgumentException("Value was either too large or too small for an Int24.");

            Value = value;
        }

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="style">The style.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static bool TryParse(string s, NumberStyles style, IFormatProvider formatProvider, out Int24 result)
        {
            int output;
            bool parse = Int32.TryParse(s, style, formatProvider, out output);

            if (!parse)
            {
                result = new Int24();
                return false;
            }

            if (output > MAX_VALUE || output < MIN_VALUE)
            {
                result = new Int24();
                return false;
            }

            result = new Int24(output);
            return true;
        }

        /// <summary>
        /// Converts to a 3 byte array.
        /// </summary>
        /// <returns>
        /// A 3 byte long array.
        /// </returns>
        public byte[] ToByteArray()
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            byte[] returnByteArr = new byte[3];

            for(int i = 0; i < returnByteArr.Length; i++)
            {
                returnByteArr[i] = bytes[i];
            }

            return returnByteArr;
        }
    }

    /// <summary>
    /// UInt24 struct.
    /// </summary>
    public struct UInt24
    {
        private uint _value;

        /// <summary>
        /// Int24 unsigned type max value.
        /// </summary>
        public const int MAX_VALUE = 16777215;
        /// <summary>
        /// Int24 unsigned type min value.
        /// </summary>
        public const int MIN_VALUE = 0;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public uint Value
        {
            get { return _value; }
            set
            {
                if (value > MAX_VALUE || value < MIN_VALUE)
                    throw new ArgumentException("Value was either too large or too small for an UInt24.");

                _value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UInt24"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public UInt24(uint value = (uint)0) : this()
        {
            if (value > MAX_VALUE || value < MIN_VALUE)
                throw new ArgumentException("Value was either too large or too small for an UInt24.");

            Value = value;
        }

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="style">The style.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static bool TryParse(string s, NumberStyles style, IFormatProvider formatProvider, out UInt24 result)
        {
            uint output;
            bool parse = UInt32.TryParse(s, style, formatProvider, out output);

            if (!parse)
            {
                result = new UInt24();
                return false;
            }

            if(output > MAX_VALUE || output < MIN_VALUE)
            {
                result = new UInt24();
                return false;
            }

            result = new UInt24(output);
            return true;
        }

        /// <summary>
        /// Converts to a 3 byte array.
        /// </summary>
        /// <returns>
        /// A 3 byte long array.
        /// </returns>
        public byte[] ToByteArray()
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            byte[] returnByteArr = new byte[3];

            for(int i = 0; i < returnByteArr.Length; i++)
            {
                returnByteArr[i] = bytes[i];
            }

            return returnByteArr;
        }
    }
}