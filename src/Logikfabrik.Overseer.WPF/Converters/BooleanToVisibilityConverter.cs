// <copyright file="BooleanToVisibilityConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// The <see cref="BooleanToVisibilityConverter" /> class.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityConverter" /> class.
        /// </summary>
        public BooleanToVisibilityConverter()
        {
            WhenTrue = Visibility.Visible;
            WhenFalse = Visibility.Collapsed;
        }

        /// <summary>
        /// Gets or sets the <see cref="Visibility" /> when <c>true</c>.
        /// </summary>
        /// <value>
        /// The <see cref="Visibility" /> when <c>true</c>.
        /// </value>
        public Visibility WhenTrue { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Visibility" /> when <c>false</c>.
        /// </summary>
        /// <value>
        /// The <see cref="Visibility" /> when <c>false</c>.
        /// </value>
        public Visibility WhenFalse { get; set; }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <c>null</c>, the valid <c>null</c> value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as bool?;

            if (!v.HasValue)
            {
                return null;
            }

            return v.Value ? WhenTrue : WhenFalse;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <c>null</c>, the valid <c>null</c> value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as Visibility?;

            if (!v.HasValue)
            {
                return null;
            }

            if (v.Value == WhenTrue)
            {
                return true;
            }

            if (v.Value == WhenFalse)
            {
                return false;
            }

            return null;
        }
    }
}