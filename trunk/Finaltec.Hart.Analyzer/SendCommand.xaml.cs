using System;
using System.Windows.Controls;
using System.Windows.Input;
using Finaltec.Hart.Analyzer.ViewModel;

namespace Finaltec.Hart.Analyzer.View
{
    /// <summary>
    /// Interaktionslogik für SendCommand.xaml
    /// </summary>
    public partial class SendCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendCommand"/> class.
        /// </summary>
        public SendCommand()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// Commands the got focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CommandGotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            tbCommand.SelectAll();
        }

        /// <summary>
        /// Window is loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WindowLoaded(object sender, EventArgs e)
        {
            ((SendCommandModel) DataContext).RawDataCleared += SwitchFocus;
        }

        /// <summary>
        /// Window is unloaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WindowUnloaded(object sender, EventArgs e)
        {
            ((SendCommandModel)DataContext).RawDataCleared -= SwitchFocus;
        }

        /// <summary>
        /// Switches the focus.
        /// </summary>
        private void SwitchFocus()
        {
            MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
        }
    }
}
