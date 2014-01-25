using System;

namespace Communication.HartLite
{
    public class ResponseCode
    {
        private readonly byte _firstByte;
        private readonly byte _secondByte;

        public byte FirstByte
        {
            get { return _firstByte; }
        }

        public byte SecondByte
        {
            get { return _secondByte; }
        }

        private ResponseCode(byte firstByte, byte secondByte)
        {
            _firstByte = firstByte;
            _secondByte = secondByte;
        }

        public static ResponseCode ToResponseCode(byte[] responseCodeBytes)
        {
            if (responseCodeBytes.Length != 2)
                throw new ArgumentException("ResponseCode needs exactly two bytes.", "responseCodeBytes");

            return new ResponseCode(responseCodeBytes[0], responseCodeBytes[1]);
        }

        public byte this[int i]
        {
            get
            {
                if(i != 0 && i != 1)
                    throw new IndexOutOfRangeException();

                return i == 0 ? FirstByte : SecondByte;
            }
        }
    }
}