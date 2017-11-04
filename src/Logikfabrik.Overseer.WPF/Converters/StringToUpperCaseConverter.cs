namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class StringToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as string;

            return v?.ToUpper(culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
