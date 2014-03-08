using Cinch;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.Services
{
    public class TestCommonServices : ITestCommonServices
    {
        public IMessageBoxService MessageBoxService { get; private set; }
        public IUIVisualizerService UiVisualizerService { get; private set; }
        public IViewAwareStatus ViewAwareStatus { get; private set; }
        public IRegionManager RegionManager { get; private set; }

        TestUIVisualizerService ITestCommonServices.UiVisualizerService { get { return (TestUIVisualizerService) UiVisualizerService; } }
        TestViewAwareStatus ITestCommonServices.ViewAwareStatus { get { return (TestViewAwareStatus) ViewAwareStatus; } }
        TestRegionManager ITestCommonServices.RegionManager { get { return (TestRegionManager) RegionManager; } }
        TestMessageBoxService ITestCommonServices.MessageBoxService { get { return (TestMessageBoxService) MessageBoxService; } }

        public TestCommonServices()
        {
            MessageBoxService = new TestMessageBoxService();
            UiVisualizerService = new TestUIVisualizerService();
            ViewAwareStatus = new TestViewAwareStatus();
            RegionManager = new TestRegionManager();
        }
    }
}