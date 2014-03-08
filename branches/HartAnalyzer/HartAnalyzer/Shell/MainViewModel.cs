using System;
using System.ComponentModel.Composition;
using Cinch;
using HartAnalyzer.Infrastructure;
using HartAnalyzer.Services;
using MEFedMVVM.ViewModelLocator;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.Shell
{
    [ExportViewModel("MainViewModel")]
    public class MainViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public MainViewModel(ICommonServices commonServices)
        {
            _commonServices = commonServices;
            
            _commonServices.ViewAwareStatus.ViewLoaded += ViewAwareStatusOnViewLoaded;
        }

        private void ViewAwareStatusOnViewLoaded()
        {
            _commonServices.RegionManager.RequestNavigate(RegionNames.RibbonRegion, new Uri(typeof(RibbonView).FullName, UriKind.Relative));
            _commonServices.RegionManager.RequestNavigate(RegionNames.StatusBarRegion, new Uri(typeof(StatusBarView).FullName, UriKind.Relative));
        }

        private readonly ICommonServices _commonServices;
    }
}