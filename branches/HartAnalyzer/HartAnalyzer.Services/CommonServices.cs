using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.ViewModelLocator;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.Services
{
    [ExportService(ServiceType.Both, typeof(ICommonServices))]
    public class CommonServices : ICommonServices
    {
        [Import(typeof(IMessageBoxService))]
        public IMessageBoxService MessageBoxService { get; set; }
        [Import(typeof(IUIVisualizerService))]
        public IUIVisualizerService UiVisualizerService { get; set; }
        [Import(typeof(IViewAwareStatus))]
        public IViewAwareStatus ViewAwareStatus { get; set; }

        [Import(typeof(IRegionManager))]
        public IRegionManager RegionManager { get; set; }
    }
}