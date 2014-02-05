using System.ComponentModel.Composition;
using HartAnalyzer.Infrastructure;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.Modules.Common
{
    [ModuleExport(typeof(CommonModule))]
    public class CommonModule : IModule
    {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public CommonModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, () => new RibbonView());
            _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, () => new StatusBarView());
        }
    }
}