using System;
using System.Windows;
using System.Windows.Controls;

namespace Finaltec.Hart.Analyzer.View.UserControls
{
    /// <summary>
    /// This delegetage represents a selected text changed handle.
    /// </summary>
    public delegate void SelectedTextChanged(object sender, string newValue);

    /// <summary>
    /// ExtendedTextBox class. 
    /// Implements the <see cref="TextBox"/> control and the <see cref="IDisposable"/> interface.
    /// </summary>
    public class ExtendedTextBox : TextBox, IDisposable
    {
        public static readonly DependencyProperty SelectedDataValueProperty =
            DependencyProperty.Register("SelectedDataValue", typeof (string), 
            typeof (ExtendedTextBox), new PropertyMetadata(default(string)));

        /// <summary>
        /// Gets or sets the selected data value.
        /// </summary>
        /// <value>The selected data value.</value>
        public string SelectedDataValue
        {
            get { return (string) GetValue(SelectedDataValueProperty); }
            set { SetValue(SelectedDataValueProperty, value); }
        }

        /// <summary>
        /// Occurs when selected text changed.
        /// </summary>
        public event SelectedTextChanged SelectedTextChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedTextBox"/> class.
        /// </summary>
        public ExtendedTextBox()
        {
            SelectionChanged += SelectedTextChangedHandel;
        }

        /// <summary>
        /// Handels the <see cref="SelectedTextChanged"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SelectedTextChangedHandel(object sender, RoutedEventArgs e)
        {
            SelectedDataValue = SelectedText;

            if(SelectedTextChanged != null)
                SelectedTextChanged.Invoke(this, SelectedDataValue);
        }

        /// <summary>
        /// Dispose the current control and release the memory usage.
        /// </summary>
        public void Dispose()
        {
            SelectionChanged -= SelectedTextChangedHandel;
        }
    }
}