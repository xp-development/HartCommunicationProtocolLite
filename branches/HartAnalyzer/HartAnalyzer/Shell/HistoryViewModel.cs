using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using Cinch;
using Communication.Hart;
using HartAnalyzer.Services;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.Shell
{
    [ExportViewModel("HistoryViewModel")]
    public class HistoryViewModel : ViewModelBase
    {
        public ObservableCollection<CommandViewModel> Items
        {
            get { return _items; }
        }

        [ImportingConstructor]
        public HistoryViewModel(IApplicationServices applicationServices)
        {
            _applicationServices = applicationServices;
            _applicationServices.HartCommunicationService.SendingCommand += HartCommunicationServiceOnSendingCommand;
            _applicationServices.HartCommunicationService.Receive += HartCommunicationServiceOnReceive;

            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        private void HartCommunicationServiceOnSendingCommand(object sender, CommandRequest args)
        {
            _dispatcher.Invoke(() => Items.Add(new CommandViewModel(args)));
        }

        private void HartCommunicationServiceOnReceive(object sender, CommandResult args)
        {
            _dispatcher.Invoke(() => Items.Add(new CommandViewModel(args)));
        }

        private readonly IApplicationServices _applicationServices;
        private readonly ObservableCollection<CommandViewModel> _items = new ObservableCollection<CommandViewModel>();
        private readonly Dispatcher _dispatcher;
    }
}