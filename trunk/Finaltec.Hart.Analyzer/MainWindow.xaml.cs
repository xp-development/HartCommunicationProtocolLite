using System.Collections.Specialized;
using Finaltec.Hart.Analyzer.ViewModel;
using Finaltec.Hart.Analyzer.ViewModel.DataModels;

namespace Finaltec.Hart.Analyzer.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Window is loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WindowLoaded(object sender, System.EventArgs e)
        {
            DataTransferModel.GetInstance().Output.CollectionChanged += LogUpdated;
        }

        /// <summary>
        /// Window is unloaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WindowUnloaded(object sender, System.EventArgs e)
        {
            DataTransferModel.GetInstance().Output.CollectionChanged -= LogUpdated;
        }

        /// <summary>
        /// Log was updated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void LogUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            lvOutput.ScrollIntoView(lvOutput.Items[lvOutput.Items.Count -1]);
        }

        /// <summary>
        /// ExtendedTextBox selected text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="newValue">The new value.</param>
        private void ExtendedTextBoxSelectedTextChanged(object sender, string newValue)
        {
            ((MainViewModel) DataContext).SelectedOutput = newValue;
        }
    }
}
