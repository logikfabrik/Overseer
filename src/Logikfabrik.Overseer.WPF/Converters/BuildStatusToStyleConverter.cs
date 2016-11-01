// <copyright file="BuildStatusToStyleConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// The <see cref="BuildStatusToStyleConverter" /> class.
    /// </summary>
    public class BuildStatusToStyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var element = (FrameworkElement)values[0];

            var status = (BuildStatus?)values[1];

            if (!status.HasValue)
            {
                return null;
            }

            var resourceKey = string.Format((string)parameter, status.Value);

            var resource = element.TryFindResource(resourceKey);

            return resource as Style;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
