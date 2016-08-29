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
        private static Brush GetBrush(Color color)
        {
            return new SolidColorBrush(color);
        }

        public static Brush ControlFocus => GetBrush(CommonColors.ControlFocus);

        public static Brush InputControlSelection => GetBrush(CommonColors.ControlFocus);
    }
}
