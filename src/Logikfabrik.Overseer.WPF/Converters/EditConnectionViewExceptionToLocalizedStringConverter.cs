// <copyright file="EditConnectionViewExceptionToLocalizedStringConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Localization;

    /// <summary>
    /// The <see cref="EditConnectionViewExceptionToLocalizedStringConverter" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditConnectionViewExceptionToLocalizedStringConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as Exception;

            return EditConnectionViewErrorLocalizer.Localize(v);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
