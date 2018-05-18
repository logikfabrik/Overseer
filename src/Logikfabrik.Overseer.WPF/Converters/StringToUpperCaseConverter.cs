// <copyright file="StringToUpperCaseConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// The <see cref="StringToUpperCaseConverter" /> class.
    /// </summary>
    /// <remarks>
    /// Based on SO https://stackoverflow.com/a/24956232, answered by kidshaw, https://stackoverflow.com/users/3836669/kidshaw.
    /// </remarks>
    // ReSharper disable once InheritdocConsiderUsage
    public class StringToUpperCaseConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as string;

            return v?.ToUpper(culture);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}