using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// TypeParser class.
    /// </summary>
    public static class TypeParser
    {
        #region Type Dictionary

        private static readonly Dictionary<string, Type> TypeDic = new Dictionary<string, Type>
                                                                       {
                                                                           //1 byte
                                                                           {"byte", typeof(byte)},

                                                                           //2 bytes
                                                                           {"ushort", typeof(ushort)},
                                                                           {"uint16", typeof(ushort)},
                                                                           {"short", typeof(short)},
                                                                           {"int16", typeof(short)},

                                                                           //3 bytes
                                                                           {"uint24", typeof(UInt24)},
                                                                           {"int24", typeof(Int24)},

                                                                           //4 bytes
                                                                           {"uint", typeof(uint)},
                                                                           {"uint32", typeof(uint)},
                                                                           {"int", typeof(int)},
                                                                           {"int32", typeof(int)},
                                                                           {"float", typeof(float)},

                                                                           //undifine size
                                                                           {"packed", typeof(object)},
                                                                           {"string", typeof(string)}
                                                                       };

        #endregion

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static ParseReturnValue TryParse(string value, Type type = null)
        {
            value = value.Trim();

            if (value.StartsWith("\"") && type == null)
                return ParseString(value);

            if (value.StartsWith("'") && type == null)
                return ParsePackedAscii(value);

            if(type == null)
            {
                string castType = "byte";

                if(value.StartsWith("(") && value.Contains(")"))
                {
                    castType = value.Substring(value.IndexOf("(") + 1, value.IndexOf(")") - 1).ToLower();
                    value = value.Remove(value.IndexOf("("), value.IndexOf(")") - value.IndexOf("(") + 1);
                }

                type = !TypeDic.ContainsKey(castType) ? typeof (byte) : TypeDic[castType];
            }

            switch (type.Name.ToLower())
            {
                case "byte":
                    return ParseByte(value);
                case "uint16":
                    return ParseUnsignedShort(value);
                case "int16":
                    return ParseShort(value);
                case "uint24":
                    return ParseUnsignedInt24(value);
                case "int24":
                    return ParseInt24(value);
                case "uint32":
                    return ParseUnsignedInt(value);
                case "int32":
                    return ParseInt(value);
                case "single":
                    return ParseFloat(value);
                case "object":
                    return ParsePackedAscii(value);
                case "string":
                    return ParseString(value);
                default:
                    return new ParseReturnValue {ErrorMessage = "Invalid type selected.", ParseResult = false};
            }
        }

        /// <summary>
        /// Parses the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseByte(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue();
            Byte outValue;

            parseReturnValue.ParseResult = byte.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out outValue);
            parseReturnValue.ParseValue = outValue;

            if (!parseReturnValue.ParseResult)
            {
                parseReturnValue.ErrorMessage = SetErrorMessage(value, typeof(byte), byte.MinValue, byte.MaxValue);
            }
            else
            {
                parseReturnValue.ByteArray = new[] { outValue };
            }
            
            return parseReturnValue;
        }

        /// <summary>
        /// Parses the unsigned short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseUnsignedShort(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue();
            ushort ushortOutValue;

            parseReturnValue.ParseResult = ushort.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out ushortOutValue);
            parseReturnValue.ParseValue = ushortOutValue;

            if (!parseReturnValue.ParseResult)
            {
                parseReturnValue.ErrorMessage = SetErrorMessage(value, typeof(ushort), ushort.MinValue, ushort.MaxValue);
            }
            else
            {
                parseReturnValue.ByteArray = BitConverter.GetBytes((ushort)parseReturnValue.ParseValue);
            }

            return parseReturnValue;
        }

        /// <summary>
        /// Parses the short.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseShort(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue();
            short shortOutValue;

            parseReturnValue.ParseResult = short.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.Number, null, out shortOutValue);
            parseReturnValue.ParseValue = shortOutValue;

            if (!parseReturnValue.ParseResult)
            {
                parseReturnValue.ErrorMessage = SetErrorMessage(value, typeof(short), short.MinValue, short.MaxValue, NumberStyles.Number);
            }
            else
            {
                parseReturnValue.ByteArray = BitConverter.GetBytes((short)parseReturnValue.ParseValue);
            }

            return parseReturnValue;
        }

        /// <summary>
        /// Parses the unsigned int24.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseUnsignedInt24(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue();
            UInt24 uint24OutValue;

            parseReturnValue.ParseResult = UInt24.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out uint24OutValue);
            parseReturnValue.ParseValue = uint24OutValue.Value;

            if (!parseReturnValue.ParseResult)
            {
                parseReturnValue.ErrorMessage = SetErrorMessage(value, typeof(UInt24), UInt24.MinValue, UInt24.MaxValue);
            }
            else
            {
                parseReturnValue.ByteArray = new UInt24((uint)parseReturnValue.ParseValue).ToByteArray();
            }

            return parseReturnValue;
        }

        /// <summary>
        /// Parses the int24.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseInt24(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue();
            Int24 int24OutValue;

            parseReturnValue.ParseResult = Int24.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.Number, null, out int24OutValue);
            parseReturnValue.ParseValue = int24OutValue.Value;

            if (!parseReturnValue.ParseResult)
            {
                parseReturnValue.ErrorMessage = SetErrorMessage(value, typeof(Int24), Int24.MinValue, Int24.MaxValue, NumberStyles.Number);
            }
            else
            {
                parseReturnValue.ByteArray = new Int24((int)parseReturnValue.ParseValue).ToByteArray();
            }

            return parseReturnValue;
        }

        /// <summary>
        /// Parses the unsigned int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseUnsignedInt(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue();
            uint uintOutValue;

            parseReturnValue.ParseResult = uint.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out uintOutValue);
            parseReturnValue.ParseValue = uintOutValue;

            if (!parseReturnValue.ParseResult)
            {
                parseReturnValue.ErrorMessage = SetErrorMessage(value, typeof(uint), uint.MinValue, uint.MaxValue);
            }
            else
            {
                parseReturnValue.ByteArray = BitConverter.GetBytes((uint)parseReturnValue.ParseValue);
            }

            return parseReturnValue;
        }

        /// <summary>
        /// Parses the int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseInt(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue();
            int intOutValue;

            parseReturnValue.ParseResult = int.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.Number, null, out intOutValue);
            parseReturnValue.ParseValue = intOutValue;

            if (!parseReturnValue.ParseResult)
            {
                parseReturnValue.ErrorMessage = SetErrorMessage(value, typeof(int), int.MinValue, int.MaxValue, NumberStyles.Number);
            }
            else
            {
                parseReturnValue.ByteArray = BitConverter.GetBytes((int)parseReturnValue.ParseValue);
            }

            return parseReturnValue;
        }

        /// <summary>
        /// Parses the float.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseFloat(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue();
            float floatOutValue;

            parseReturnValue.ParseResult = float.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.Float, null, out floatOutValue);
            parseReturnValue.ParseValue = floatOutValue;

            if (!parseReturnValue.ParseResult)
            {
                double doubleOutValue;
                bool doubleParse = double.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : NumberStyles.Float, null, out doubleOutValue);

                if (doubleParse)
                {
                    if (doubleOutValue > float.MaxValue)
                    {
                        parseReturnValue.ErrorMessage = string.Format("Value '{0}' is too large for an Float.", value);
                    }

                    if (doubleOutValue < float.MinValue)
                    {
                        parseReturnValue.ErrorMessage = string.Format("Value '{0}' is too small for an Float.", value);
                    }
                }
                else
                {
                    parseReturnValue.ErrorMessage = string.Format("Value '{0}' was not in a correct format for an Float.", value);
                }
            }
            else
            {
                parseReturnValue.ByteArray = BitConverter.GetBytes((float)parseReturnValue.ParseValue);
            }

            return parseReturnValue;
        }

        /// <summary>
        /// Parses the packed ASCII.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParsePackedAscii(string value)
        {
            throw new NotImplementedException(string.Format("Currently not implementet. Value was {0}.", value));
        }

        /// <summary>
        /// Parses the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static ParseReturnValue ParseString(string value)
        {
            ParseReturnValue parseReturnValue = new ParseReturnValue { ParseResult = true };

            if (value.StartsWith("\""))
            {
                if (!value.EndsWith("\"") || !value.Remove(0, 1).Contains("\""))
                {
                    parseReturnValue.ErrorMessage = string.Format("\" missing on end of string '{0}'.", value.Remove(0, 1));
                    parseReturnValue.ParseResult = false;
                }
                else
                {
                    parseReturnValue.ByteArray = value.Remove(value.Length - 1, 1).Remove(0, 1).ToCharArray().Select(c => (byte)c).ToArray();
                    parseReturnValue.ParseValue = value.Remove(value.Length - 1, 1).Remove(0, 1);
                }
            }
            else
            {
                parseReturnValue.ByteArray = value.ToCharArray().Select(c => (byte)c).ToArray();
                parseReturnValue.ParseValue = value;
            }
            
            return parseReturnValue;
        }

        /// <summary>
        /// Sets the error message.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <param name="minValue">The min value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <param name="numberstyle">The numberstyle.</param>
        /// <returns></returns>
        private static string SetErrorMessage(string value, Type type, long minValue, long maxValue, NumberStyles numberstyle = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite)
        {
            long l;
            bool parse = long.TryParse((value.StartsWith("0x")) ? value.Replace("0x", "") : value, (value.StartsWith("0x")) ? NumberStyles.HexNumber : numberstyle, null, out l);

            if (parse)
            {
                if (l > maxValue)
                {
                    return string.Format("Value '{0}' is too large for an {1}.", value, type.Name);
                }

                if (l < minValue)
                {
                    return string.Format("Value '{0}' is too small for an {1}.", value, type.Name);
                }

                return string.Empty;
            }
            
            return string.Format("Value '{0}' was not in a correct format for an {1}.", value, type.Name);
        }
    }
}