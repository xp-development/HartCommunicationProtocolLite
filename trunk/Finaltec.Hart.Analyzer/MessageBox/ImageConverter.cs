using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Finaltec.Hart.Analyzer.View.MessageBox
{
    /// <summary>
    /// One-way converter from System.Drawing.Image to System.Windows.Media.ImageSource
    /// </summary>
    [ValueConversion(typeof(Bitmap), typeof(System.Windows.Media.ImageSource))]
    public class ImageConverter : IValueConverter
    {
        /// <summary>
        /// Converter for a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The convert parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>
        /// A convertet value. If the convertation failed the value will be NULL.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return null; }

            if (typeof(BitmapImage) != value.GetType())
            {
                var image = (Image)value;
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Bmp);
                memoryStream.Seek(0, SeekOrigin.Begin);
                bitmap.StreamSource = memoryStream;
                bitmap.EndInit();
                return bitmap;
            }

            return value as BitmapImage;
        }

        /// <summary>
        /// Converter for a value. RETURNS ONLY NULL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The convert parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>
        /// RETURNS ONLY NULL.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}