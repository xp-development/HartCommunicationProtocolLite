using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Threading;
using Cinch;
using Communication.Hart;
using HartAnalyzer.Infrastructure;
using HartAnalyzer.Modules.Common.ConnectionConfiguration;
using HartAnalyzer.Services;
using MEFedMVVM.ViewModelLocator;
using XpDevelopment.Core.Notifications;
using XpDevelopment.Presentation;

namespace HartAnalyzer.Modules.Common
{
    [ExportViewModel("RibbonViewModel")]
    public class RibbonViewModel : ViewModelBase
    {
        public AsyncCommand<object, object> ConnectionCommand { get; private set; }
        public SimpleCommand<object, object> ConfigurateConnectionCommand { get; private set; }

        public PortState PortState
        {
            get { return _portState; }
            set
            {
                _portState = value;
                NotifyPropertyChanged("PortState");
            }
        }

        [ImportingConstructor]
        public RibbonViewModel(IApplicationServices applicationServices, ICommonServices commonServices)
        {
            _applicationServices = applicationServices;
            _commonServices = commonServices;

            _currentDispatcher = Dispatcher.CurrentDispatcher;

            _applicationServices.HartCommunicationService.Subscribe(item => item.PortState).OnChange(item => PortState = item, _currentDispatcher);

            ConnectionCommand = new AsyncCommand<object, object>(OnConnect, arg => !IsConnectionChanging());
            ConfigurateConnectionCommand = new SimpleCommand<object, object>(item => PortState == PortState.Closed, OnConfigurationConnection);
        }

        private void OnConfigurationConnection(object item)
        {
            _commonServices.UiVisualizerService.ShowDialog(ViewNames.ConnectionConfigurationView, new ConnectionConfigurationViewModel());
        }

        private bool IsConnectionChanging()
        {
            return _applicationServices.HartCommunicationService.PortState == PortState.Opening ||
                   _applicationServices.HartCommunicationService.PortState == PortState.Closing;
        }

        private async Task OnConnect(object arg)
        {
            if (_applicationServices.HartCommunicationService.PortState == PortState.Closed)
            {
                var openResult = await _applicationServices.HartCommunicationService.OpenAsync();
                if (openResult != OpenResult.Opened)
                    _currentDispatcher.Invoke(() => _commonServices.MessageBoxService.ShowInformation(openResult.ToString()));
            }
            else if (_applicationServices.HartCommunicationService.PortState == PortState.Opened)
            {
                var closeResult = await _applicationServices.HartCommunicationService.CloseAsync();
                if (closeResult != CloseResult.Closed)
                    _currentDispatcher.Invoke(() => _commonServices.MessageBoxService.ShowInformation(closeResult.ToString()));
            }
        }

        private readonly Dispatcher _currentDispatcher;
        private PortState _portState;

        private readonly IApplicationServices _applicationServices;
        private readonly ICommonServices _commonServices;
    }
}