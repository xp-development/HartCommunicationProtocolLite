using System.Windows;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// Interface IView.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Occurs on loaded.
        /// </summary>
        event RoutedEventHandler Loaded;
        /// <summary>
        /// Occurs on closing.
        /// </summary>
        event System.ComponentModel.CancelEventHandler Closing;

        /// <summary>
        /// Sets the dialog result.
        /// </summary>
        /// <value>The dialog result.</value>
        bool? DialogResult { set; }
        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        /// <value>The data context.</value>
        object DataContext { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        IView Owner { get; set; }
        /// <summary>
        /// Gets or sets the view startup location.
        /// </summary>
        /// <value>The view startup location.</value>
        WindowStartupLocation WindowStartupLocation { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        double Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        double Height { get; set; }
        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        /// <value>The state of the window.</value>
        WindowState WindowState { get; set; }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        void Close();
        /// <summary>
        /// Shows this instance.
        /// </summary>
        void Show();
        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();
    }
}