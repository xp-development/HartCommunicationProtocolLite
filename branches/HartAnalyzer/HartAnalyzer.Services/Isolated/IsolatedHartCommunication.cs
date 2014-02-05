using System;
using System.Threading;
using Communication.Hart;

namespace HartAnalyzer.Services
{
    public class IsolatedHartCommunication : IHartCommunication
    {
        private IAddress _currentAddress;
        public event ReceiveHandler Receive;
        public event SendingCommandHandler SendingCommand;

        public IsolatedHartCommunication()
        {
            PreambleLength = 5;
            MaxNumberOfRetries = 2;
            Timeout = TimeSpan.FromSeconds(5);
            AutomaticZeroCommand = true;
            PortName = "COM1";
        }

        public int PreambleLength { get; set; }
        public int MaxNumberOfRetries { get; set; }
        public TimeSpan Timeout { get; set; }
        public bool AutomaticZeroCommand { get; set; }

        public IAddress Address
        {
            get { return _currentAddress; }
        }

        public string PortName { get; set; }

        public OpenResult Open()
        {
            _currentAddress = null;

            Thread.Sleep(1000);
            return OpenResult.Opened;
        }

        public CloseResult Close()
        {
            Thread.Sleep(1000);
            return CloseResult.Closed;
        }

        private CommandResult SendCommand(byte commandNumber, byte[] data)
        {
            if (commandNumber == 0)
                _currentAddress = new ShortAddress(0);

            if (AutomaticZeroCommand && commandNumber != 0 && !(_currentAddress is LongAddress))
                SendZeroCommand();

            Command command;
            if (commandNumber == 0)
            {
                command = Command.Zero(PreambleLength);
                _currentAddress = new LongAddress(0, 1, new byte[] { 2, 3, 4 });
            }
            else
                command = new Command(PreambleLength, _currentAddress, commandNumber, new byte[0], data);

            if (SendingCommand != null)
                SendingCommand.BeginInvoke(this, new CommandRequest(command), null, null);

            command.ResponseCode = new byte[] { 0, 0 };

            if (Receive != null)
                Receive.BeginInvoke(this, new CommandResult(command), null, null);

            return new CommandResult(command);
        }

        public CommandResult Send(byte command)
        {
            return SendCommand(command, new byte[0]);
        }

        public CommandResult Send(byte command, byte[] data)
        {
            return SendCommand(command, data);
        }

        public CommandResult SendZeroCommand()
        {
            return SendCommand(0, new byte[0]);
        }

        public void SendAsync(byte command)
        {
            Action action = () => SendCommand(command, new byte[0]);
            action.BeginInvoke(null, null);
        }

        public void SendAsync(byte command, byte[] data)
        {
            Action action = () => SendCommand(command, data);
            action.BeginInvoke(null, null);
        }

        public void SendZeroCommandAsync()
        {
            Action action = () => SendCommand(0, new byte[0]);
            action.BeginInvoke(null, null);
        }

        public void SwitchAddressTo(IAddress address)
        {
            throw new NotImplementedException();
        }

        public void Initialize(string comPort)
        { }
    }
}
