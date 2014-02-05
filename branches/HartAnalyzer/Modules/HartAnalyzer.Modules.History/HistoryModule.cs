using System.ComponentModel.Composition;
using HartAnalyzer.Infrastructure;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.Modules.History
{
    [ModuleExport(typeof(HistoryModule))]
    public class HistoryModule : IModule
    {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public HistoryModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            var view = new HistoryView();

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, () => view);
        }
    }
}