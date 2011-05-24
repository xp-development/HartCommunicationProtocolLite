using System.Windows.Input;

namespace Finaltec.Hart.Analyzer.View
{
    /// <summary>
    /// Interaktionslogik für SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsDialog"/> class.
        /// </summary>
        public SettingsDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
