using Cinch;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.Services
{
    public interface ICommonServices
    {
        IMessageBoxService MessageBoxService { get; }
        IUIVisualizerService UiVisualizerService { get; }
        IViewAwareStatus ViewAwareStatus { get; }

        IRegionManager RegionManager { get; }
    }
}