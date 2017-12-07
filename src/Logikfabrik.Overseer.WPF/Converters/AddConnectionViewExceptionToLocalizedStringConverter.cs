// <copyright file="AddConnectionViewExceptionToLocalizedStringConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Localization;

    /// <summary>
    /// The <see cref="AddConnectionViewExceptionToLocalizedStringConverter" /> class.
    /// </summary>
    public class AddConnectionViewExceptionToLocalizedStringConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as Exception;

            return AddConnectionViewErrorLocalizer.Localize(v);
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
