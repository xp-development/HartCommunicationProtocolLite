using System;
using Cinch;
using Communication.Hart;

namespace HartAnalyzer.SpecificCommands
{
    public class CommandViewModel : ViewModelBase
    {
        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                NotifyPropertyChanged("Time");
            }
        }

        public CommandType CommandType
        {
            get { return _commandType; }
            set
            {
                _commandType = value;
                NotifyPropertyChanged("CommandType");
            }
        }

        public byte[] Preambles
        {
            get { return _preambles; }
            set
            {
                _preambles = value;
                NotifyPropertyChanged("Preambles");
            }
        }

        public byte[] Delimiter
        {
            get { return _delimiter; }
            set
            {
                _delimiter = value;
                NotifyPropertyChanged("Delimiter");
            }
        }

        public byte[] Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }

        public byte[] Command
        {
            get { return _command; }
            set
            {
                _command = value;
                NotifyPropertyChanged("Command");
            }
        }

        public byte[] Data
        {
            get { return _data; }
            set
            {
                _data = value;
                NotifyPropertyChanged("Data");
            }
        }

        public byte[] Length
        {
            get { return _length; }
            set
            {
                _length = value;
                NotifyPropertyChanged("Length");
            }
        }

        public byte[] CheckSum
        {
            get { return _checkSum; }
            set
            {
                _checkSum = value;
                NotifyPropertyChanged("CheckSum");
            }
        }

        public byte[] ResponseCode
        {
            get { return _responseCode; }
            set
            {
                _responseCode = value;
                NotifyPropertyChanged("ResponseCode");
            }
        }

        private DateTime _time;
        private CommandType _commandType;
        private byte[] _preambles;
        private byte[] _delimiter;
        private byte[] _address;
        private byte[] _command;
        private byte[] _data;
        private byte[] _length;
        private byte[] _checkSum;
        private byte[] _responseCode;

        public CommandViewModel(CommandRequest args)
        {
            _time = DateTime.Now;
            _commandType = CommandType.Send;

            _preambles = new byte[args.PreambleLength];
            for (var i = 0; i < _preambles.Length; i++)
                _preambles[i] = 0xFF;

            _delimiter = new[] {args.Delimiter};
            _address = args.Address.ToByteArray();
            _command = new[] {args.CommandNumber};
            _length = new[]{(byte) args.Data.Length};
            _data = args.Data;
            _checkSum = new[] {args.Checksum};
        }

        public CommandViewModel(CommandResult args)
        {
            _time = DateTime.Now;
            _commandType = CommandType.Receive;

            _preambles = new byte[args.PreambleLength];
            for (var i = 0; i < _preambles.Length; i++)
                _preambles[i] = 0xFF;

            _delimiter = new[] { args.Delimiter };
            _address = args.Address.ToByteArray();
            _command = new[] { args.CommandNumber };
            _length = new[] { (byte)args.Data.Length };
            _data = args.Data;
            _checkSum = new[] { args.Checksum };
            _responseCode = new[] {args.ResponseCode.FirstByte, args.ResponseCode.SecondByte};
        }
    }
}