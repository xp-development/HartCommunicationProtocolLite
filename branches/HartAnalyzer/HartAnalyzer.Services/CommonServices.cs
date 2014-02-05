using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.Services
{
    [ExportService(ServiceType.Both, typeof(ICommonServices))]
    public class CommonServices : ICommonServices
    {
        [Import(typeof(IMessageBoxService))]
        public IMessageBoxService MessageBoxService { get; set; }
        [Import(typeof(IUIVisualizerService))]
        public IUIVisualizerService UiVisualizerService { get; set; }
    }
}