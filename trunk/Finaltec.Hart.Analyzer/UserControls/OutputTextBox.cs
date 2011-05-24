using System;
using System.Windows;
using System.Windows.Controls;

namespace Finaltec.Hart.Analyzer.View.UserControls
{
    /// <summary>
    /// OutputTextBox control.
    /// Implements control TextBox and interface IDisposable.
    /// </summary>
    public class OutputTextBox : TextBox, IDisposable
    {
        /// <summary>
        /// SelectedValueProperty DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty SelectedValueProperty =
                DependencyProperty.Register("SelectedValue",
                typeof(String), typeof(OutputTextBox), new UIPropertyMetadata());

        /// <summary>
        /// Gets or sets the selected value.
        /// </summary>
        /// <value>The selected value.</value>
        public string SelectedValue
        {
            get { return (string) GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputTextBox"/> class.
        /// </summary>
        public OutputTextBox()
        {
            SelectionChanged += SelectionChangedEventHandle;
        }

        /// <summary>
        /// Dispose the current control and release the memory usage.
        /// </summary>
        public void Dispose()
        {
            SelectionChanged -= SelectionChangedEventHandle;
        }

        /// <summary>
        /// Selections the changed event handle.
        /// NOTE: Needs currently .NET Framework 4.0
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SelectionChangedEventHandle(object sender, RoutedEventArgs e)
        {
            SetCurrentValue(SelectedValueProperty, SelectedText);
        }
    }
}