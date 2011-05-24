using System;

namespace Finaltec.Communication.HartLite
{
    public class LongAddress : IAddress
    {
        private byte _currentAddressIndex;

        private byte _manufacturerIdentificationCode;
        private byte _manufacturerDeviceTypeCode;
        private readonly byte[] _deviceIdentificationNumber;

        public LongAddress(byte manufacturerIdentificationCode, byte manufacturerDeviceTypeCode, byte[] deviceIdentificationNumber)
        {
            if (deviceIdentificationNumber.Length != 3)
                throw new ArgumentException();

            _currentAddressIndex = 0;

            _manufacturerIdentificationCode = manufacturerIdentificationCode;
            _manufacturerDeviceTypeCode = manufacturerDeviceTypeCode;
            _deviceIdentificationNumber = deviceIdentificationNumber;
        }

        public byte[] ToByteArray()
        {
            byte[] address = new byte[5];
            address[0] = GetManufacturerIdentificationCode();
            address[1] = _manufacturerDeviceTypeCode;
            address[2] = _deviceIdentificationNumber[0];
            address[3] = _deviceIdentificationNumber[1];
            address[4] = _deviceIdentificationNumber[2];
            return address;
        }

        public void SetNextByte(byte nextByte)
        {
            if (_currentAddressIndex == 0)
                _manufacturerIdentificationCode = nextByte;
            if (_currentAddressIndex == 1)
                _manufacturerDeviceTypeCode = nextByte;
            if (_currentAddressIndex == 2)
                _deviceIdentificationNumber[0] = nextByte;
            if (_currentAddressIndex == 3)
                _deviceIdentificationNumber[1] = nextByte;
            if (_currentAddressIndex == 4)
                _deviceIdentificationNumber[2] = nextByte;

            ++_currentAddressIndex;
        }

        public static LongAddress Empty
        {
            get
            {
                return new LongAddress(0, 0, new byte[3]);
            }
        }

        public byte this[int index]
        {
            get
            {
                if (index == 0)
                    return GetManufacturerIdentificationCode();
                if (index == 1)
                    return _manufacturerDeviceTypeCode;
                if (index == 2)
                    return _deviceIdentificationNumber[0];
                if (index == 3)
                    return _deviceIdentificationNumber[1];
                if (index == 4)
                    return _deviceIdentificationNumber[2];

                throw new IndexOutOfRangeException();
            }
        }

        private byte GetManufacturerIdentificationCode()
        {
            return (byte)((_manufacturerIdentificationCode | 0x80) & 0xBF);
        }
    }
}