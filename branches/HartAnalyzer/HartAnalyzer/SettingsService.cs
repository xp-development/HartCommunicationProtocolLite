using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using HartAnalyzer.Properties;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer
{
    [ExportService(ServiceType.Both, typeof(ISettingsService))]
    public class SettingsService : ISettingsService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowChecksumFilter
        {
            get { return _settings.ShowChecksumFilter; }
            set
            {
                _settings.ShowChecksumFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowResponseCodeFilter
        {
            get { return _settings.ShowResponseCodeFilter; }
            set
            {
                _settings.ShowResponseCodeFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowDataFilter
        {
            get { return _settings.ShowDataFilter; }
            set
            {
                _settings.ShowDataFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowLengthFilter
        {
            get { return _settings.ShowLengthFilter; }
            set
            {
                _settings.ShowLengthFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowCommandFilter
        {
            get { return _settings.ShowCommandFilter; }
            set
            {
                _settings.ShowCommandFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowAddressFilter
        {
            get { return _settings.ShowAddressFilter; }
            set
            {
                _settings.ShowAddressFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowDelimiterFilter
        {
            get { return _settings.ShowDelimiterFilter; }
            set
            {
                _settings.ShowDelimiterFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowPreamblesFilter
        {
            get { return _settings.ShowPreamblesFilter; }
            set
            {
                _settings.ShowPreamblesFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowTypeFilter
        {
            get { return _settings.ShowTypeFilter; }
            set
            {
                _settings.ShowTypeFilter = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowTimeFilter
        {
            get { return _settings.ShowTimeFilter; }
            set
            {
                _settings.ShowTimeFilter = value;
                NotifyPropertyChanged();
            }
        }

        public string CommandFilter
        {
            get { return _settings.CommandFilter; }
            set
            {
                _settings.CommandFilter = value;
                NotifyPropertyChanged();
            }
        }

        public WindowState MainViewWindowState
        {
            get { return _settings.MainViewWindowState; }
            set
            {
                _settings.MainViewWindowState = value;
                NotifyPropertyChanged();
            }
        }

        public int MainViewLeft
        {
            get { return _settings.MainViewLeft; }
            set
            {
                _settings.MainViewLeft = value;
                NotifyPropertyChanged();
            }
        }

        public int MainViewTop
        {
            get { return _settings.MainViewTop; }
            set
            {
                _settings.MainViewTop = value;
                NotifyPropertyChanged();
            }
        }

        public int MainViewHeight
        {
            get { return _settings.MainViewHeight; }
            set
            {
                _settings.MainViewHeight = value;
                NotifyPropertyChanged();
            }
        }

        public int MainViewWidth
        {
            get { return _settings.MainViewWidth; }
            set
            {
                _settings.MainViewWidth = value;
                NotifyPropertyChanged();
            }
        }

        public SettingsService()
        {
            _settings = Settings.Default;
        }

        [NotifyPropertyChangedInvocator]
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly Settings _settings;
    }
}