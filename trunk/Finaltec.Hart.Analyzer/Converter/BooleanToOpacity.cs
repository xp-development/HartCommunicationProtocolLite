using System;
using System.Globalization;
using System.Windows.Data;

namespace Finaltec.Hart.Analyzer.View.Converter
{
    /// <summary>
    /// Converts bool values to opacity values. 
    /// Implements interface IValueConverter.
    /// </summary>
    public class BooleanToOpacity : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that should be converted.</param>
        /// <param name="targetType">The target type for the convert.</param>
        /// <param name="parameter">Possible convert parameter.</param>
        /// <param name="culture">The converter culture.</param>
        /// <returns>
        /// A Visibility object with Visible or Collapsed.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(value is bool) || (bool) value) ? 1 : 0.5;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that should be converted.</param>
        /// <param name="targetType">The target type for the convert.</param>
        /// <param name="parameter">Possible convert parameter.</param>
        /// <param name="culture">The converter culture.</param>
        /// <returns>
        /// The original object value parameter.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}