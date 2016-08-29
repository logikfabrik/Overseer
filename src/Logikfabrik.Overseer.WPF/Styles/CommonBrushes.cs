// <copyright file="CommonBrushes.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Styles
{
    using System.Windows.Media;

    /// <summary>
    /// The <see cref="CommonBrushes" /> class.
    /// </summary>
    public static class CommonBrushes
    {
        /// <summary>
        /// Gets the control focus brush.
        /// </summary>
        /// <value>
        /// The control focus brush.
        /// </value>
        public static Brush ControlFocus => GetBrush(CommonColors.ControlFocus);

        /// <summary>
        /// Gets the input control selection brush.
        /// </summary>
        /// <value>
        /// The input control selection brush.
        /// </value>
        public static Brush InputControlSelection => GetBrush(CommonColors.ControlFocus);

        private static Brush GetBrush(Color color)
        {
            return new SolidColorBrush(color);
        }
    }
}
