using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using log4net;

namespace Finaltec.Hart.Analyzer.ViewModel.DataModels
{
    /// <summary>
    /// Input class.
    /// Implements interface <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class Input : INotifyPropertyChanged 
    {
        private readonly ObservableCollection<Input> _parentList;

        private string _rawValue;
        private InputDataType _type;
        private bool _isValid;

        /// <summary>
        /// Gets or sets the raw value.
        /// </summary>
        /// <value>The raw value.</value>
        public string RawValue
        {
            get { return _rawValue; }
            set
            {
                _rawValue = value;
                Input input = _parentList[_parentList.Count -1];

                if (!string.IsNullOrEmpty(input.RawValue))
                    _parentList.Add(new Input(_parentList, InputType.Byte));

                if(input != this && string.IsNullOrEmpty(_rawValue))
                    _parentList.Remove(this);

                IsValid = Validate();
            }
        }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public InputDataType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                InvokePropertyChanged("Type");

                IsValid = Validate();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                InvokePropertyChanged("IsValid");
            }
        }

        /// <summary>
        /// Gets or sets the validate error message.
        /// </summary>
        /// <value>The validate error message.</value>
        public string ValidateErrorMessage { get; private set; }
        /// <summary>
        /// Gets or sets the data bytes.
        /// </summary>
        /// <value>The data bytes.</value>
        public byte[] DataBytes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> class.
        /// </summary>
        /// <param name="parentList">The parent list.</param>
        /// <param name="type">The type.</param>
        public Input(ObservableCollection<Input> parentList, InputType type)
        {
            _parentList = parentList;
            Type = new InputDataType(type);
            Type.PropertyChanged += delegate
                                        {
                                            IsValid = Validate(); 
                                            InvokePropertyChanged("Type");
                                        };
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            if (RawValue == null)
                return true;

            ParseReturnValue returnValue = TypeParser.TryParse(RawValue, GetCurrentType());
            ValidateErrorMessage = returnValue.ErrorMessage;
            DataBytes = returnValue.ByteArray;

            return returnValue.ParseResult;
        }

        /// <summary>
        /// Gets the type of the current instance.
        /// </summary>
        /// <returns></returns>
        private Type GetCurrentType()
        {
            switch (Type.InputType)
            {
                case InputType.Byte:
                    return typeof (byte);
                case InputType.UShort:
                    return typeof(ushort);
                case InputType.Short:
                    return typeof(short);
                case InputType.UInt24:
                    return typeof(UInt24);
                case InputType.Int24:
                    return typeof(Int24);
                case InputType.UInt:
                    return typeof(uint);
                case InputType.Int:
                    return typeof(int);
                case InputType.Float:
                    return typeof(float);
                case InputType.String:
                    return typeof(string);
            }

            return typeof(object);
        }

        /// <summary>
        /// Invokes the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void InvokePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occuse if some property changed his value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// InputType enum.
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// Byte data type.
        /// </summary>
        Byte,
        /// <summary>
        /// Unsigned short data type.
        /// </summary>
        UShort,
        /// <summary>
        /// Short data type.
        /// </summary>
        Short,
        /// <summary>
        /// Unsigend integer with 24 bit data type.
        /// </summary>
        UInt24,
        /// <summary>
        /// Integer with 24 bit data type.
        /// </summary>
        Int24,
        /// <summary>
        /// Unsigned integer data type.
        /// </summary>
        UInt,
        /// <summary>
        /// Integer data type.
        /// </summary>
        Int,
        /// <summary>
        /// Float data type.
        /// </summary>
        Float,
        /// <summary>
        /// String data type.
        /// </summary>
        String
    }

    /// <summary>
    /// InputDataType class.
    /// Implements interface <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class InputDataType : INotifyPropertyChanged
    {
        private bool _isByte = true;
        private bool _isUShort;
        private bool _isShort;
        private bool _isUInt24;
        private bool _isInt24;
        private bool _isUInt;
        private bool _isInt;
        private bool _isFloat;
        private bool _isString;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is byte.
        /// </summary>
        /// <value><c>true</c> if this instance is byte; otherwise, <c>false</c>.</value>
        public bool IsByte
        {
            get { return _isByte; }
            set
            {
                if(value)
                {
                    InputType = InputType.Byte;
                    SetType();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is U short.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is U short; otherwise, <c>false</c>.
        /// </value>
        public bool IsUShort
        {
            get { return _isUShort; }
            set
            {
                if (value)
                {
                    InputType = InputType.UShort;
                    SetType();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is short.
        /// </summary>
        /// <value><c>true</c> if this instance is short; otherwise, <c>false</c>.</value>
        public bool IsShort
        {
            get { return _isShort; }
            set
            {
                if (value)
                {
                    InputType = InputType.Short;
                    SetType();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is U int24.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is U int24; otherwise, <c>false</c>.
        /// </value>
        public bool IsUInt24
        {
            get { return _isUInt24; }
            set
            {
                if (value)
                {
                    InputType = InputType.UInt24;
                    SetType();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is int24.
        /// </summary>
        /// <value><c>true</c> if this instance is int24; otherwise, <c>false</c>.</value>
        public bool IsInt24
        {
            get { return _isInt24; }
            set
            {
                if (value)
                {
                    InputType = InputType.Int24;
                    SetType();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is U int.
        /// </summary>
        /// <value><c>true</c> if this instance is U int; otherwise, <c>false</c>.</value>
        public bool IsUInt
        {
            get { return _isUInt; }
            set
            {
                if (value)
                {
                    InputType = InputType.UInt;
                    SetType();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is int.
        /// </summary>
        /// <value><c>true</c> if this instance is int; otherwise, <c>false</c>.</value>
        public bool IsInt
        {
            get { return _isInt; }
            set
            {
                if (value)
                {
                    InputType = InputType.Int;
                    SetType();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is float.
        /// </summary>
        /// <value><c>true</c> if this instance is float; otherwise, <c>false</c>.</value>
        public bool IsFloat
        {
            get { return _isFloat; }
            set
            {
                if (value)
                {
                    InputType = InputType.Float;
                    SetType();
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is string.
        /// </summary>
        /// <value><c>true</c> if this instance is string; otherwise, <c>false</c>.</value>
        public bool IsString
        {
            get { return _isString; }
            set
            {
                if (value)
                {
                    InputType = InputType.String;
                    SetType();
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the input.
        /// </summary>
        /// <value>The type of the input.</value>
        public InputType InputType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputDataType"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public InputDataType(InputType type)
        {
            InputType = type;
        }

        /// <summary>
        /// Sets the type.
        /// </summary>
        private void SetType()
        {
            _isByte = false;
            _isUShort = false;
            _isShort = false;
            _isUInt24 = false;
            _isInt24 = false;
            _isUInt = false;
            _isInt = false;
            _isFloat = false;
            _isString = false;

            switch (InputType)
            {
                case InputType.Byte:
                    _isByte = true;
                    break;
                case InputType.UShort:
                    _isUShort = true;
                    break;
                case InputType.Short:
                    _isShort = true;
                    _isUShort = true;
                    break;
                case InputType.UInt24:
                    _isUInt24 = true;
                    break;
                case InputType.Int24:
                    _isInt24 = true;
                    _isUInt24 = true;
                    break;
                case InputType.UInt:
                    _isUInt = true;
                    break;
                case InputType.Int:
                    _isInt = true;
                    _isUInt = true;
                    break;
                case InputType.Float:
                    _isFloat = true;
                    break;
                case InputType.String:
                    _isString = true;
                    break;
            }

            InvokePropertyChanged("IsByte");
            InvokePropertyChanged("IsUShort");
            InvokePropertyChanged("IsShort");
            InvokePropertyChanged("IsUInt24");
            InvokePropertyChanged("IsInt24");
            InvokePropertyChanged("IsUInt");
            InvokePropertyChanged("IsInt");
            InvokePropertyChanged("IsFloat");
            InvokePropertyChanged("IsString");
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return InputType.ToString();
        }

        /// <summary>
        /// Invokes the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void InvokePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occuse if some property changed his value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}