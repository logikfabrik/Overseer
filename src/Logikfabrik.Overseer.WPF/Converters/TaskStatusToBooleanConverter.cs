// <copyright file="TaskStatusToBooleanConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="TaskStatusToBooleanConverter" /> class.
    /// </summary>
    public class TaskStatusToBooleanConverter : BooleanToVisibilityConverter
    {
        /// <summary>
        /// Gets or sets the <see cref="TaskStatus" /> which evaluates to <c>true</c>.
        /// </summary>
        /// <value>
        /// The <see cref="TaskStatus" /> which evaluates to <c>true</c>.
        /// </value>
        public TaskStatus TrueFor { get; set; }

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
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as TaskStatus?;

            return base.Convert(v == TrueFor, targetType, parameter, culture);
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
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = base.ConvertBack(value, targetType, parameter, culture) as bool?;

            if (!v.HasValue)
            {
                return null;
            }

            if (v.Value)
            {
                return TrueFor;
            }

            return null;
        }
    }
}