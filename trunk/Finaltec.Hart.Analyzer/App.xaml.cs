using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using Finaltec.Hart.Analyzer.ViewModel;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using log4net;

namespace Finaltec.Hart.Analyzer.View
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (App));

        /// <summary>
        /// Called on application startup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                InitLogging();

                ViewProvider viewProvider = GetViewProvider();
                MainViewModel mainViewModel = new MainViewModel(viewProvider);
                mainViewModel.InitView();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Unknown Exception Occurs. Application will Shutdown.\n\n" + ex.Message,
                                               "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gets the view provider.
        /// </summary>
        /// <returns></returns>
        private static ViewProvider GetViewProvider()
        {
            ViewProvider viewProvider = new ViewProvider();
            viewProvider.AddView("SettingsView", typeof(SettingsDialog));
            viewProvider.AddView("SendCommand", typeof(SendCommand));
            viewProvider.AddView("MainView", typeof(MainWindow));
            viewProvider.AddView("MessageBox", typeof(MessageBox.MessageBox));
            viewProvider.AddView("AboutDialog", typeof(AboutDialog));

            Log.Info("ViewProvider created.");

            return viewProvider;
        }

        /// <summary>
        /// Inits the logging.
        /// </summary>
        private static void InitLogging()
        {
            Log.Info(string.Format("{0}{0}## - Start application at {1}.{0}", Environment.NewLine, DateTime.Now.ToString(CultureInfo.InvariantCulture)));
            Log.Info("Log4net initialized successfully from app config file.");
        }
    }
}
