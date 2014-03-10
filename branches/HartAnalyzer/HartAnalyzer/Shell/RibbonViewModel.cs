using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Cinch;
using Communication.Hart;
using HartAnalyzer.ConnectionConfiguration;
using HartAnalyzer.Infrastructure;
using HartAnalyzer.Services;
using MEFedMVVM.ViewModelLocator;
using XpDevelopment.Presentation;

namespace HartAnalyzer.Shell
{
    [ExportViewModel("RibbonViewModel")]
    public class RibbonViewModel : ViewModelBase
    {
        public AsyncCommand<object, object> ConnectionCommand { get; private set; }
        public SimpleCommand<object, object> ConfigurateConnectionCommand { get; private set; }

        public AsyncCommand<object, object> SendCommand0 { get; private set; }
        public AsyncCommand<object, object> SendCommand1 { get; private set; }
        public AsyncCommand<object, object> SendCommand2 { get; private set; }

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
            _dispatcher = Dispatcher.CurrentDispatcher;

            _applicationServices.HartCommunicationService.PropertyChanged += HartCommunicationServiceOnPropertyChanged;

            PortState = _applicationServices.HartCommunicationService.PortState;

            ConnectionCommand = new AsyncCommand<object, object>(OnConnect, arg => !IsConnectionChanging());
            ConfigurateConnectionCommand = new SimpleCommand<object, object>(item => PortState == PortState.Closed, OnConfigurationConnection);

            CreateSendCommands();
        }

        private void CreateSendCommands()
        {
            SendCommand0 = new AsyncCommand<object, object>(args => _applicationServices.HartCommunicationService.Send(0), args => IsPortOpened());
            SendCommand1 = new AsyncCommand<object, object>(args => _applicationServices.HartCommunicationService.Send(1), args => IsPortOpened());
            SendCommand2 = new AsyncCommand<object, object>(args => _applicationServices.HartCommunicationService.Send(2), args => IsPortOpened());
        }

        private bool IsPortOpened()
        {
            return _applicationServices.HartCommunicationService.PortState == PortState.Opened;
        }

        private void HartCommunicationServiceOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "PortState")
                _dispatcher.Invoke(() => PortState = _applicationServices.HartCommunicationService.PortState);
        }

        private void OnConfigurationConnection(object item)
        {
            _commonServices.UiVisualizerService.ShowDialog(ViewNames.ConnectionConfigurationView, new ConnectionConfigurationViewModel(_applicationServices));
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
                    _commonServices.MessageBoxService.ShowInformation(openResult.ToString());
                 CommandManager.InvalidateRequerySuggested();
            }
            else if (_applicationServices.HartCommunicationService.PortState == PortState.Opened)
            {
                var closeResult = await _applicationServices.HartCommunicationService.CloseAsync();
                if (closeResult != CloseResult.Closed)
                    _commonServices.MessageBoxService.ShowInformation(closeResult.ToString());
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private PortState _portState;
        private readonly IApplicationServices _applicationServices;
        private readonly ICommonServices _commonServices;
        private readonly Dispatcher _dispatcher;
    }
}