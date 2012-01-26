using System;
using System.Reflection;
using Finaltec.Hart.Analyzer.ViewModel.Common;

namespace Finaltec.Hart.Analyzer.ViewModel
{
    public class AboutDialogViewModel : ViewModelBase
    {
        private string _productVersion;

        public string ProductVersion
        {
            get { return _productVersion; }
            set
            { 
                _productVersion = string.Format("Version: {0}", value);
                InvokePropertyChanged("ProductVersion");
            }
        }

        public AboutDialogViewModel(ViewProvider viewProvider) : base(viewProvider)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            ProductVersion = string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }
    }
}