// <copyright file="CommonColors.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Styles
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// The <see cref="CommonColors" /> class.
    /// </summary>
    public static class CommonColors
    {
        private const string Green = "#FF99BB00";
        private const string LightGreen = "#FFAADD00";
        private const string DarkGray = "#FF3E3E3E";
        private const string Gray = "#FF636363";
        private const string LightGray = "#FF838383";
        private const string White = "#FFFFFFFF";
        private const string Black = "#FF000000";

        private static Color GetColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Color cannot be null or white space.", nameof(color));
            }

            return (Color)ColorConverter.ConvertFromString(color);
        }

        public static Color ControlFocus => GetColor(LightGreen);

        public static Color ControlStaticBorder => GetColor(LightGray);

        public static Color ControlStaticBackground => GetColor(LightGray);

        public static Color ControlStaticForeground => GetColor(White);

        public static Color ControlMouseOverBorder => GetColor(LightGray);

        public static Color ControlMouseOverBackground => GetColor(LightGray);

        public static Color ControlMouseOverForeground => GetColor(White);

        public static Color ControlPressedBorder => GetColor(LightGreen);

        public static Color ControlPressedBackground => GetColor(LightGreen);

        public static Color ControlPressedForeground => GetColor(White);

        public static Color ControlDisabledBorder => GetColor(LightGray);

        public static Color ControlDisabledBackground => GetColor(LightGray);

        public static Color ControlDisabledForeground => GetColor(White);


        public static Color InputControlStaticBorder => GetColor(LightGray);

        public static Color InputControlStaticBackground => GetColor(White);

        public static Color InputControlStaticForeground => GetColor(Black);

        public static Color InputControlMouseOverBorder => GetColor(LightGray);

        public static Color InputControlMouseOverBackground => GetColor(White);

        public static Color InputControlMouseOverForeground => GetColor(Black);

        public static Color InputControlFocusBorder => GetColor(LightGreen);

        public static Color InputControlFocusBackground => GetColor(White);

        public static Color InputControlFocusForeground => GetColor(Black);

        public static Color InputControlDisabledBorder => GetColor(LightGray);

        public static Color InputControlDisabledBackground => GetColor(White);

        public static Color InputControlDisabledForeground => GetColor(Black);
    }
}
