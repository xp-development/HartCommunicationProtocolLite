using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using Cinch;
using HartAnalyzer.Services;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.Shell
{
    [ExportViewModel("StatusBarViewModel")]
    public class StatusBarViewModel : ViewModelBase
    {
        public PortState PortState
        {
            get { return _portState; }
            set
            {
                _portState = value;
                NotifyPropertyChanged("PortState");
            }
        }

        public string PortName
        {
            get { return _portName; }
            set
            {
                _portName = value;
                NotifyPropertyChanged("PortName");
            }
        }

        [ImportingConstructor]
        public StatusBarViewModel(IHartCommunicationService communicationService)
        {
            _communicationService = communicationService;
            _communicationService.PropertyChanged += CommunicationServiceOnPropertyChanged;
            _dispatcher = Dispatcher.CurrentDispatcher;

            PortState = _communicationService.PortState;
            PortName = _communicationService.PortName;
        }

        private void CommunicationServiceOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "PortState")
                _dispatcher.Invoke(() => PortState = _communicationService.PortState);
            else if (propertyChangedEventArgs.PropertyName == "PortName")
                _dispatcher.Invoke(() => PortName = _communicationService.PortName);
        }

        private readonly IHartCommunicationService _communicationService;
        private PortState _portState;
        private string _portName;
        private readonly Dispatcher _dispatcher;
    }
}