using System;
using System.Windows;
using System.Windows.Controls;

namespace Finaltec.Hart.Analyzer.View.UserControls
{
    /// <summary>
    /// ByteValidationTextBox control.
    /// Implements control TextBox and interface IDisposable.
    /// </summary>
    public class ByteValidationTextBox : TextBox, IDisposable
    {
        /// <summary>
        /// IsValidProperty DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty IsValidProperty =
                DependencyProperty.Register("IsValid",
                typeof(Boolean), typeof(ByteValidationTextBox), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteValidationTextBox"/> class.
        /// </summary>
        public ByteValidationTextBox()
        {
            TextChanged += UpdateIsValid;
        }

        /// <summary>
        /// Updates the is valid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> instance containing the event data.</param>
        private void UpdateIsValid(object sender, TextChangedEventArgs e)
        {
            byte b;
            IsValid = byte.TryParse(Text, out b);

            if(Text == "0")
                SelectAll();
        }

        /// <summary>
        /// Dispose the current control and release the memory usage.
        /// </summary>
        public void Dispose()
        {
            TextChanged -= UpdateIsValid;
        }
    }
}