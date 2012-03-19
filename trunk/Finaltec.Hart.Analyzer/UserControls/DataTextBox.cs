using System;
using System.Windows;
using System.Windows.Controls;

namespace Finaltec.Hart.Analyzer.View.UserControls
{
    /// <summary>
    /// DataTextBox class. 
    /// Implements the <see cref="TextBox"/> control and the <see cref="IDisposable"/> interface.
    /// </summary>
    public class  DataTextBox : TextBox, IDisposable
    {
        private static readonly DependencyProperty WindowTopProperty =
            DependencyProperty.Register("WindowTop", typeof(double), 
            typeof(DataTextBox), new PropertyMetadata(default(double)));

        private static readonly DependencyProperty WindowLeftProperty =
            DependencyProperty.Register("WindowLeft", typeof(double), 
            typeof(DataTextBox), new PropertyMetadata(default(double)));

        private readonly DataType _dataType;

        /// <summary>
        /// Gets or sets the window top.
        /// </summary>
        /// <value>The window top.</value>
        public double WindowTop
        {
            get { return (double) GetValue(WindowTopProperty); }
            set { SetValue(WindowTopProperty, value); }
        }

        /// <summary>
        /// Gets or sets the window left.
        /// </summary>
        /// <value>The window left.</value>
        public double WindowLeft
        {
            get { return (double) GetValue(WindowLeftProperty); }
            set { SetValue(WindowLeftProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTextBox"/> class.
        /// </summary>
        public DataTextBox()
        {
            _dataType = new DataType();

            GotFocus += DataTextBoxGotFocus;
            LostFocus += DataTextBoxLostFocus;

            Loaded += DataTextBoxLoaded;
            Unloaded += DataTextBoxUnloaded;
        }

        /// <summary>
        /// Occurse on DataTextBox is loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DataTextBoxLoaded(object sender, RoutedEventArgs e)
        {
            _dataType.SetDataContext(DataContext);
        }

        /// <summary>
        /// Occurse on DataTextBox is unloaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DataTextBoxUnloaded(object sender, RoutedEventArgs e)
        {
            _dataType.Hide();
        }

        /// <summary>
        /// Occurse on DataTextBox got the focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DataTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            SelectAll();

            _dataType.Top = WindowTop;
            _dataType.Left = WindowLeft - 104;
            _dataType.Show();

            Focus();
        }

        /// <summary>
        /// Occurse on DataTextBox lost his focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DataTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            _dataType.Hide();
        }

        /// <summary>
        /// Dispose the current control and release the memory usage.
        /// </summary>
        public void Dispose()
        {
            GotFocus -= DataTextBoxGotFocus;
            LostFocus -= DataTextBoxLostFocus;

            Loaded -= DataTextBoxLoaded;
            Unloaded -= DataTextBoxUnloaded;
        }
    }
}