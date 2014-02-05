using System.ComponentModel.Composition;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.Services
{
    [ExportService(ServiceType.Both, typeof(IApplicationServices))]
    public class ApplicationServices : IApplicationServices
    {
        [Import(typeof(IHartCommunicationService))]
        public IHartCommunicationService HartCommunicationService { get; set; }
    }
}