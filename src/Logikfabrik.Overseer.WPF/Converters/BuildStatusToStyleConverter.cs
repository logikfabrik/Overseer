// <copyright file="BuildStatusToStyleConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    // TODO: Move to Logikfabrik.Overseer.WPF.Client or remove/rework?

    /// <summary>
    /// The <see cref="BuildStatusToStyleConverter" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildStatusToStyleConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var element = (FrameworkElement)values[0];

            var status = values[1] as BuildStatus?;

            if (!status.HasValue)
            {
                return null;
            }

            var resourceKey = string.Format((string)parameter, status.Value);

            var resource = element.TryFindResource(resourceKey);

            return resource as Style;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
