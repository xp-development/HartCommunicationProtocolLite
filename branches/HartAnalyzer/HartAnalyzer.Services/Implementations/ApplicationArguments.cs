using System;
using System.Linq;
using MEFedMVVM.ViewModelLocator;

namespace HartAnalyzer.Services
{
    [ExportService(ServiceType.Both, typeof(IApplicationArguments))]
    public class ApplicationArguments : IApplicationArguments
    {
        public bool IsIsolatedTestModeEnabled { get { return _commandLineArgs.Any(item => item == "/isolated"); } }

        public ApplicationArguments()
        {
            _commandLineArgs = Environment.GetCommandLineArgs();
        }

        private readonly string[] _commandLineArgs;
    }
}