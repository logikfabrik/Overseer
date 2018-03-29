// <copyright file="IsLessThanConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// The <see cref="IsLessThanConverter" /> class.
    /// </summary>
    public class IsLessThanConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as double?;

            if (!v.HasValue)
            {
                return false;
            }

            double p;

            if (!double.TryParse(parameter?.ToString(), out p))
            {
                return false;
            }

            return v < p;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
