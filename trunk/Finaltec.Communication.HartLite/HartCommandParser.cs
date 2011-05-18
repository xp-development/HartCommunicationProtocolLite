using System;
using log4net;

namespace Finaltec.Communication.HartLite
{
    internal class HartCommandParser
    {
        private enum ReceiveState
        {
            NotInCommand,
            Preamble,
            StartDelimiter,
            Address,
            Command,
            DataLength,
            Data,
            Checksum
        }

        private static readonly ILog Log = LogManager.GetLogger(typeof(HartCommandParser));
        private ReceiveState _currentReceiveState = ReceiveState.NotInCommand;
        private Command _currentCommand;
        private int _currentIndex;

        public event Action<Command> CommandComplete;

        public void ParseNextBytes(Byte[] data)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                ParseByte(data[i]);
            }
        }

        private void ParseByte(byte data)
        {
            switch (_currentReceiveState)
            {
                case ReceiveState.NotInCommand:
                    if (data == 0xFF)
                    {
                        _currentReceiveState = ReceiveState.Preamble;
                        ParsePreamble(data);
                    }
                    break;
                case ReceiveState.Preamble:
                    ParsePreamble(data);
                    break;
                case ReceiveState.StartDelimiter:
                    ParseStartDelimiter(data);
                    break;
                case ReceiveState.Address:
                    ParseAddress(data);
                    break;
                case ReceiveState.Command:
                    ParseCommand(data);
                    break;
                case ReceiveState.DataLength:
                    ParseDataLength(data);
                    break;
                case ReceiveState.Data:
                    ParseData(data);
                    break;
                case ReceiveState.Checksum:
                    ParseChecksum(data);
                    break;
            }
        }

        private void ParseCommand(byte data)
        {
            _currentCommand.CommandNumber = data;
            _currentReceiveState = ReceiveState.DataLength;
            _currentIndex = 0;
        }

        private void ParseChecksum(byte data)
        {
            _currentIndex = 0;
            _currentReceiveState = ReceiveState.NotInCommand;

            if (_currentCommand.IsChecksumCorrect(data))
            {
                OnCommandComplete();
                _currentReceiveState = ReceiveState.NotInCommand;
            }
            else
            {
                Log.Warn("Checksum is wrong!");
                _currentCommand.Data = new byte[0];
                CommandComplete(_currentCommand);
            }
        }

        private void OnCommandComplete()
        {
            if (CommandComplete != null)
                CommandComplete(_currentCommand);
        }

        private void ParseData(byte data)
        {
            if (_currentIndex < 2)
            {
                _currentCommand.ResponseCode[_currentIndex] = data;
            }
            else
            {
                _currentCommand.Data[_currentIndex - 2] = data;
            }
            _currentIndex++;

            if (_currentIndex == _currentCommand.Data.Length + _currentCommand.ResponseCode.Length)
            {
                _currentReceiveState = ReceiveState.Checksum;
            }
        }

        private void ParseDataLength(byte dataLength)
        {
            if (dataLength == 1)
            {
                Reset();
            }

            if (dataLength == 0)
            {
                _currentCommand.ResponseCode = new byte[0];
                _currentCommand.Data = new byte[0];
                _currentReceiveState = ReceiveState.Checksum;
            }
            else
            {
                _currentCommand.ResponseCode = new byte[2];
                _currentCommand.Data = new byte[dataLength - 2];
                _currentReceiveState = ReceiveState.Data;
            }

            _currentIndex = 0;
        }

        private void ParseAddress(byte data)
        {
            _currentCommand.Address[_currentIndex] = data;
            _currentIndex++;

            if (_currentCommand.Address.Length == _currentIndex)
            {
                _currentReceiveState = ReceiveState.Command;
                _currentIndex = 0;
            }
        }

        private void ParseStartDelimiter(byte data)
        {
            int addressLength = (data == Command.SlaveToMasterStartDelimiter ? 1 : 5);
            _currentCommand.Address = new byte[addressLength];
            _currentCommand.StartDelimiter = data;
            if (_currentCommand.StartDelimiter == 0x86 || _currentCommand.StartDelimiter == 0x06)
            {
                _currentReceiveState = ReceiveState.Address;
                _currentIndex = 0;
            }
            else
            {
                Reset();
            }
        }

        private void ParsePreamble(byte data)
        {
            _currentIndex++;
            if (data != 255)
            {
                _currentCommand = new Command();
                _currentReceiveState = ReceiveState.StartDelimiter;
                _currentCommand.PreambleLength = _currentIndex;
                _currentIndex = 0;
                if (_currentCommand.PreambleLength < 2)
                {
                    Reset();
                    return;
                }
                ParseByte(data);
            }
        }

        public void Reset()
        {
            _currentCommand = new Command();
            _currentIndex = 0;
            _currentReceiveState = ReceiveState.NotInCommand;
        }
    }
}