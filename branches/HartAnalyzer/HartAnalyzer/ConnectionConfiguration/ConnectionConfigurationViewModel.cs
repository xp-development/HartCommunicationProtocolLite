using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using HartAnalyzer.Services;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.ConnectionConfiguration
{
    [ExportViewModel("ConnectionConfigurationViewModel")]
    public class ConnectionConfigurationViewModel : EditableValidatingViewModelBase, IViewStatusAwareInjectionAware
    {
        public DataWrapper<string> SelectedPortName
        {
            get { return _selectedPortName; }
            set
            {
                if (_selectedPortName.DataValue == value.DataValue)
                    return;

                _selectedPortName = value;
                NotifyPropertyChanged("SelectedPortName");
            }
        }

        public ObservableCollection<string> PossiblePortNames { get; private set; }

        public SimpleCommand<object, object> SaveCommand { get; private set; }
        public SimpleCommand<object, object> CancelCommand { get; private set; }

        [ImportingConstructor]
        public ConnectionConfigurationViewModel(IApplicationServices applicationServices)
        {
            _hartCommunicationService = applicationServices.HartCommunicationService;
            PossiblePortNames = new ObservableCollection<string>(_hartCommunicationService.PossiblePortNames);

            _selectedPortName = new DataWrapper<string>(this, _selectedPortChangeArgs, () =>
                {
                    if (_selectedPortName != null)
                        _hartCommunicationService.PortName = _selectedPortName.DataValue;
                })
                {
                    DataValue = _hartCommunicationService.PortName
                };

            _cachedListOfDataWrappers = DataWrapperHelper.GetWrapperProperties(this);

            SaveCommand = new SimpleCommand<object, object>(item =>
                {
                    EndEdit();
                    CloseActivePopUpCommand.Execute(true);
                });

            CancelCommand = new SimpleCommand<object, object>(item =>
                {
                    CancelEdit();
                    CloseActivePopUpCommand.Execute(true);
                });
        }

        protected override void OnBeginEdit()
        {
            base.OnBeginEdit();
            DataWrapperHelper.SetBeginEdit(_cachedListOfDataWrappers);
        }

        protected override void OnEndEdit()
        {
            base.OnEndEdit();
            DataWrapperHelper.SetEndEdit(_cachedListOfDataWrappers);
        }

        protected override void OnCancelEdit()
        {
            base.OnCancelEdit();
            DataWrapperHelper.SetCancelEdit(_cachedListOfDataWrappers);
        }

        public void InitialiseViewAwareService(IViewAwareStatus viewAwareStatusService)
        {
            viewAwareStatusService.ViewLoaded += BeginEdit;
        }

        private static readonly PropertyChangedEventArgs _selectedPortChangeArgs = ObservableHelper.CreateArgs<ConnectionConfigurationViewModel>(x => x.SelectedPortName);
        private readonly IHartCommunicationService _hartCommunicationService;
        private DataWrapper<string> _selectedPortName;
        private readonly IEnumerable<DataWrapperBase> _cachedListOfDataWrappers;
    }
}