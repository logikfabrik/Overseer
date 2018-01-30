// <copyright file="TaskStatusToBooleanConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Data;

    /// <summary>
    /// The <see cref="TaskStatusToBooleanConverter" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class TaskStatusToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the <see cref="TaskStatus" /> which evaluates to <c>true</c>.
        /// </summary>
        /// <value>
        /// The <see cref="TaskStatus" /> which evaluates to <c>true</c>.
        /// </value>
        public TaskStatus[] TrueFor { get; set; } = { };

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as TaskStatus?;

            if (TrueFor == null || !v.HasValue)
            {
                return false;
            }

            return TrueFor.Contains(v.Value);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}