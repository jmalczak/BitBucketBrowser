namespace BitBucketBrowser.Infrastructure.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;

            if (boolValue)
            {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
        } 

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibilityValue = (Visibility)value;

            if (visibilityValue == Visibility.Visible)
            {
                return true;
            }

            return false;
        }
    }
}