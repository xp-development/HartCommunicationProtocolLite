using System.ComponentModel.Composition;
using Cinch;
using HartAnalyzer.Services;
using MEFedMVVM.ViewModelLocator;
using XpDevelopment.Core.Notifications;

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

            _communicationService.Subscribe(item => item.PortState).OnChange((sender, item) => PortState = item);
            _communicationService.Subscribe(item => item.PortName).OnChange((sender, item) => PortName = item);
        }

        private readonly IHartCommunicationService _communicationService;
        private PortState _portState;
        private string _portName;
    }
}