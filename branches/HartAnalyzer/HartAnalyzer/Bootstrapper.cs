using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Windows;
using Cinch;
using HartAnalyzer.Services;
using HartAnalyzer.Shell;
using MEFedMVVM.ViewModelLocator;
using Microsoft.Practices.Prism.MefExtensions;

namespace HartAnalyzer
{
    public class Bootstrapper : MefBootstrapper, IComposer, IContainerProvider
    {
        protected override DependencyObject CreateShell()
        {
            LocatorBootstrapper.ApplyComposer(this);

            return Container.GetExportedValue<MainView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            CinchBootStrapper.Initialise(new[] {Assembly.GetExecutingAssembly()});

            Application.Current.MainWindow = (MainView)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(HartCommunicationService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ViewModelBase).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ViewModelLocator).Assembly));
        }

        protected override CompositionContainer CreateContainer()
        {
            // The Prism call to create a container
            var exportProvider = new MEFedMVVMExportProvider(MEFedMVVMCatalog.CreateCatalog(AggregateCatalog));
            _compositionContainer = new CompositionContainer(exportProvider);
            exportProvider.SourceProvider = _compositionContainer;

            return _compositionContainer;
        }

        public ComposablePartCatalog InitializeContainer()
        {
            return AggregateCatalog;
        }

        public IEnumerable<ExportProvider> GetCustomExportProviders()
        {
            return null;
        }

        CompositionContainer IContainerProvider.CreateContainer()
        {
            return _compositionContainer;
        }

        private CompositionContainer _compositionContainer;
    }
}