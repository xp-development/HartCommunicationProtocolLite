using System.ComponentModel.Composition;
using System.Windows.Threading;
using Cinch;
using HartAnalyzer.Services;
using MEFedMVVM.ViewModelLocator;
using XpDevelopment.Core.Notifications;

namespace HartAnalyzer.Modules.Common
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
            _dispatcher = Dispatcher.CurrentDispatcher;

            _communicationService.Subscribe(item => item.PortState).OnChange(arg => PortState = arg, _dispatcher);
            _communicationService.Subscribe(item => item.PortName).OnChange(arg => PortName = arg, _dispatcher);
        }

        private readonly IHartCommunicationService _communicationService;
        private PortState _portState;
        private readonly Dispatcher _dispatcher;
        private string _portName;
    }
}