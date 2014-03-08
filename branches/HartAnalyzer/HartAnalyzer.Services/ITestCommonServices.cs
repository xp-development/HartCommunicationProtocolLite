using Cinch;

namespace HartAnalyzer.Services
{
    public interface ITestCommonServices : ICommonServices
    {
        new TestMessageBoxService MessageBoxService { get; }
        new TestUIVisualizerService UiVisualizerService { get; }
        new TestViewAwareStatus ViewAwareStatus { get; }

        new TestRegionManager RegionManager { get; }
    }
}