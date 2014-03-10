using System;
using System.Globalization;
using System.Windows.Data;

namespace HartAnalyzer.Converters
{
    public class ByteArrayToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Binding.DoNothing;

            if (!(value is byte[]))
                throw new NotSupportedException();

            return BitConverter.ToString((byte[]) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}