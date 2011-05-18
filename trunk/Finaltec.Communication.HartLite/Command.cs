using System;

namespace Finaltec.Communication.HartLite
{
    internal class Command
    {
        public byte[] ResponseCode { get; set; }
        public int PreambleLength { get; set; }
        public byte StartDelimiter { get; set; }
        public byte[] Address { get; set; }
        public byte CommandNumber { get; set; }
        public byte[] Data { get; set; }

        private static byte MasterToSlaveStartDelimiter
        {
            get
            {
                return 0x82; // 2
            }
        }

        public static byte SlaveToMasterStartDelimiter
        {
            get { return 6; }
        }

        public Command()
        {
        }

        public Command(int preambleLength, byte[] address, byte commandNumber, byte[] responseCode, byte[] data)
        {
            PreambleLength = preambleLength;
            Address = address;
            CommandNumber = commandNumber;
            Data = data;
            ResponseCode = responseCode;
            StartDelimiter = MasterToSlaveStartDelimiter;
        }

        public static Command Zero()
        {
            return Zero(0, 20);
        }

        public static Command Zero(int preambleLength)
        {
            return Zero(0, preambleLength);
        }

        public static Command Zero(byte address)
        {
            return Zero(address, 20);
        }

        public static Command Zero(byte address, int preambleLength)
        {
            return new Command(preambleLength, new[] { (byte) (128 + address) }, 0, new byte[0], new byte[0])
                            {
                                StartDelimiter = 2
                            };
        }

        public bool IsChecksumCorrect(byte checksum)
        {
            return CalculateChecksum() == checksum;
        }

        public Byte[] ToByteArray()
        {
            byte[] commandAsByteArray = BuildByteArray();

            commandAsByteArray[commandAsByteArray.Length - 1] = CalculateChecksum();

            return commandAsByteArray;
        }

        private byte[] BuildByteArray()
        {
            const int SIZE_OF_START_DELIMITER = 1;
            const int SIZE_OF_COMMAND_NUMBER = 1;
            const int SIZE_OF_DATA_BYTE_COUNT = 1;
            const int SIZE_OF_CHECKSUM = 1;

            int commandLength = PreambleLength + Data.Length + ResponseCode.Length + Address.Length +
                                SIZE_OF_START_DELIMITER + SIZE_OF_COMMAND_NUMBER +
                                SIZE_OF_DATA_BYTE_COUNT + SIZE_OF_CHECKSUM;
            var commandAsByteArray = new byte[commandLength];

            int currentIndex = 0;
            for (int i = 0; i < PreambleLength; ++i)
            {
                commandAsByteArray[currentIndex] = 255;
                currentIndex++;
            }
            commandAsByteArray[currentIndex] = StartDelimiter;
            currentIndex += SIZE_OF_START_DELIMITER;
            CopyArrayInArray(commandAsByteArray, Address, currentIndex);
            currentIndex += Address.Length;
            commandAsByteArray[currentIndex] = CommandNumber;
            currentIndex += SIZE_OF_COMMAND_NUMBER;
            commandAsByteArray[currentIndex] = (byte)(Data.Length + ResponseCode.Length);
            currentIndex += SIZE_OF_DATA_BYTE_COUNT;
            CopyArrayInArray(commandAsByteArray, ResponseCode, currentIndex);
            currentIndex += ResponseCode.Length;
            CopyArrayInArray(commandAsByteArray, Data, currentIndex);

            return commandAsByteArray;
        }

        private static void CopyArrayInArray(byte[] destination, byte[] source, int offset)
        {
            for (int i = 0; i < source.Length; ++i)
            {
                destination[i + offset] = source[i];
            }
        }

        internal byte CalculateChecksum()
        {
            byte[] data = BuildByteArray();
            byte checksum = 0;
            for (int i = PreambleLength; i < data.Length - 1; ++i)
            {
                checksum ^= data[i];
            }
            return checksum;
        }
    }
}