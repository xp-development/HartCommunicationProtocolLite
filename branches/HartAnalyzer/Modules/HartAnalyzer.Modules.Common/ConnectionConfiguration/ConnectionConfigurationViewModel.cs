using Cinch;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.Modules.Common.ConnectionConfiguration
{
    [ExportViewModel("ConnectionConfigurationViewModel")]
    public class ConnectionConfigurationViewModel : ViewModelBase, IViewStatusAwareInjectionAware
    {
        public void InitialiseViewAwareService(IViewAwareStatus viewAwareStatusService)
        {
            viewAwareStatusService.ViewLoaded += () =>
                {
                };
        }
    }
}