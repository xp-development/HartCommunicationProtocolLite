using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Communication.Hart;
using HartAnalyzer.Services.Properties;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.Services
{
    [ExportService(ServiceType.Both, typeof(IHartCommunicationService))]
    public class HartCommunicationService : IHartCommunicationService
    {
        public PortState PortState
        {
            get { return _portState; }
            set
            {
                if(_portState != value)
                {
                    _portState = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string PortName
        {
            get { return _communication.PortName; }
            set
            {
                if (_communication.PortName != value)
                {
                    _communication.PortName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICollection<string> PossiblePortNames { get; private set; }

        public event SendingCommandHandler SendingCommand
        {
            add { _communication.SendingCommand += value; }
            remove { _communication.SendingCommand -= value; }
        }

        public event ReceiveHandler Receive
        {
            add { _communication.Receive += value; }
            remove { _communication.Receive -= value; }
        }

        public Task Send(byte command)
        {
            return Send(command, new byte[0]);
        }

        public async Task Send(byte command, byte[] data)
        {
            await Task.Run(() => _communication.Send(command, data));
        }

        [ImportingConstructor]
        public HartCommunicationService(IApplicationArguments applicationArguments)
            : this(GetCommunication(applicationArguments))
        {
            if (applicationArguments.IsIsolatedTestModeEnabled)
                PossiblePortNames = new[] { "COM1", "COM2", "COM3" };
            else
                PossiblePortNames = SerialPort.GetPortNames();
        }

        public HartCommunicationService(IHartCommunication communication)
        {
            _communication = communication;
        }

        private static IHartCommunication GetCommunication(IApplicationArguments applicationArguments)
        {
            if (applicationArguments.IsIsolatedTestModeEnabled)
                return new IsolatedHartCommunication();

            return new HartCommunication("COM1");
        }

        public async Task<OpenResult> OpenAsync()
        {
            if (PortState == PortState.Opened)
                return OpenResult.Opened;

            PortState = PortState.Opening;

            var result = await Task.Run(() => _communication.Open());

            PortState = result == OpenResult.Opened ? PortState.Opened : PortState.Closed;

            return result;
        }

        public async Task<CloseResult> CloseAsync()
        {
            if (PortState == PortState.Closed)
                return CloseResult.Closed;

            PortState = PortState.Closing;

            var result = await Task.Run(() => _communication.Close());
            if (result == CloseResult.Closed)
                PortState = PortState.Closed;

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEventHandler changedEventHandler = _propertyChanged;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange(ref _propertyChanged, comparand + value, comparand);
                }
                while (changedEventHandler != comparand);
            }
            remove
            {
                PropertyChangedEventHandler changedEventHandler = _propertyChanged;
                PropertyChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange(ref _propertyChanged, comparand - value, comparand);
                }
                while (changedEventHandler != comparand);
            }
        }

        [NotifyPropertyChangedInvocator]
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var changedEventHandler = _propertyChanged;
            if (changedEventHandler == null)
                return;

            changedEventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IHartCommunication _communication;
        private PortState _portState;
        private PropertyChangedEventHandler _propertyChanged;
    }
}