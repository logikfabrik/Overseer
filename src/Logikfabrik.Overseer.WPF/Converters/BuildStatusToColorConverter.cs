using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Logikfabrik.Overseer.WPF.Converters
{
    [ValueConversion(typeof(BuildStatus?), typeof(Color))]
    public class BuildStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as BuildStatus?;

            if (!v.HasValue)
            {
                return null;
            }

            switch (v)
            {
                    
            }

            // TODO: This converter
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
