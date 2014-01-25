using System;

namespace Communication.HartLite
{
    public class ShortAddress : IAddress
    {
        private byte _pollingAddress;

        public ShortAddress(byte pollingAddress)
        {
            if (pollingAddress > 15 || pollingAddress < 0)
                throw new ArgumentException();

            _pollingAddress = pollingAddress;
        }

        public byte[] ToByteArray()
        {
            return new [] { GetPollingAddress() };
        }

        public void SetNextByte(byte nextByte)
        {
            _pollingAddress = nextByte;
        }

        public byte this[int index]
        {
            get
            {
                if (index == 0)
                    return GetPollingAddress();

                throw new IndexOutOfRangeException();
            }
        }

        public static ShortAddress Empty
        {
            get { return new ShortAddress(0); }
        }

        private byte GetPollingAddress()
        {
            return (byte) ((_pollingAddress | 0x80) & 0x8F);
        }
    }
}