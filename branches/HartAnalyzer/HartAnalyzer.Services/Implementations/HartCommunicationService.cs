using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Communication.Hart;
using HartAnalyzer.Services.Annotations;
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
                _portState = value;
                NotifyPropertyChanged();
            }
        }

        public string PortName
        {
            get { return _communication.PortName; }
            set
            {
                _communication.PortName = value;
                NotifyPropertyChanged();
            }
        }

        [ImportingConstructor]
        public HartCommunicationService(IApplicationArguments applicationArguments)
            : this(GetCommunication(applicationArguments))
        { }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IHartCommunication _communication;
        private PortState _portState;
    }
}