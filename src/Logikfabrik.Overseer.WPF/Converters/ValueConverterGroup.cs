// <copyright file="ValueConverterGroup.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    /// <summary>
    /// The <see cref="ValueConverterGroup" /> class.
    /// </summary>
    /// <remarks>
    /// Based on SO https://stackoverflow.com/a/8326207, answered by Town, https://stackoverflow.com/users/54975/town.
    /// </remarks>
    // ReSharper disable once InheritdocConsiderUsage
    public class ValueConverterGroup : List<IValueConverter>, IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
