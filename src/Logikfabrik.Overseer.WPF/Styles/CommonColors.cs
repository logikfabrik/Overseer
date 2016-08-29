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

        /// <summary>
        /// Gets the control focus color.
        /// </summary>
        /// <value>
        /// The control focus color.
        /// </value>
        public static Color ControlFocus => GetColor(LightGreen);

        /// <summary>
        /// Gets the control static border color.
        /// </summary>
        /// <value>
        /// The control static border color.
        /// </value>
        public static Color ControlStaticBorder => GetColor(LightGray);

        /// <summary>
        /// Gets the control static background color.
        /// </summary>
        /// <value>
        /// The control static background color.
        /// </value>
        public static Color ControlStaticBackground => GetColor(LightGray);

        /// <summary>
        /// Gets the control static foreground color.
        /// </summary>
        /// <value>
        /// The control static foreground color.
        /// </value>
        public static Color ControlStaticForeground => GetColor(White);

        /// <summary>
        /// Gets the control mouse over border color.
        /// </summary>
        /// <value>
        /// The control mouse over border color.
        /// </value>
        public static Color ControlMouseOverBorder => GetColor(LightGray);

        /// <summary>
        /// Gets the control mouse over background color.
        /// </summary>
        /// <value>
        /// The control mouse over background color.
        /// </value>
        public static Color ControlMouseOverBackground => GetColor(LightGray);

        /// <summary>
        /// Gets the control mouse over foreground color.
        /// </summary>
        /// <value>
        /// The control mouse over foreground color.
        /// </value>
        public static Color ControlMouseOverForeground => GetColor(White);

        /// <summary>
        /// Gets the control pressed border color.
        /// </summary>
        /// <value>
        /// The control pressed border color.
        /// </value>
        public static Color ControlPressedBorder => GetColor(LightGreen);

        /// <summary>
        /// Gets the control pressed background color.
        /// </summary>
        /// <value>
        /// The control pressed background color.
        /// </value>
        public static Color ControlPressedBackground => GetColor(LightGreen);

        /// <summary>
        /// Gets the control pressed foreground color.
        /// </summary>
        /// <value>
        /// The control pressed foreground color.
        /// </value>
        public static Color ControlPressedForeground => GetColor(White);

        /// <summary>
        /// Gets the control disabled border color.
        /// </summary>
        /// <value>
        /// The control disabled border color.
        /// </value>
        public static Color ControlDisabledBorder => GetColor(LightGray);

        /// <summary>
        /// Gets the control disabled background color.
        /// </summary>
        /// <value>
        /// The control disabled background color.
        /// </value>
        public static Color ControlDisabledBackground => GetColor(LightGray);

        /// <summary>
        /// Gets the control disabled foreground color.
        /// </summary>
        /// <value>
        /// The control disabled foreground color.
        /// </value>
        public static Color ControlDisabledForeground => GetColor(White);

        /// <summary>
        /// Gets the input control static border color.
        /// </summary>
        /// <value>
        /// The input control static border color.
        /// </value>
        public static Color InputControlStaticBorder => GetColor(LightGray);

        /// <summary>
        /// Gets the input control static background color.
        /// </summary>
        /// <value>
        /// The input control static background color.
        /// </value>
        public static Color InputControlStaticBackground => GetColor(White);

        /// <summary>
        /// Gets the input control static foreground color.
        /// </summary>
        /// <value>
        /// The input control static foreground color.
        /// </value>
        public static Color InputControlStaticForeground => GetColor(Black);

        /// <summary>
        /// Gets the input control mouse over border color.
        /// </summary>
        /// <value>
        /// The input control mouse over border color.
        /// </value>
        public static Color InputControlMouseOverBorder => GetColor(LightGray);

        /// <summary>
        /// Gets the input control mouse over background color.
        /// </summary>
        /// <value>
        /// The input control mouse over background color.
        /// </value>
        public static Color InputControlMouseOverBackground => GetColor(White);

        /// <summary>
        /// Gets the input control mouse over foreground color.
        /// </summary>
        /// <value>
        /// The input control mouse over foreground color.
        /// </value>
        public static Color InputControlMouseOverForeground => GetColor(Black);

        /// <summary>
        /// Gets the input control focus border color.
        /// </summary>
        /// <value>
        /// The input control focus border color.
        /// </value>
        public static Color InputControlFocusBorder => GetColor(LightGreen);

        /// <summary>
        /// Gets the input control focus background color.
        /// </summary>
        /// <value>
        /// The input control focus background color.
        /// </value>
        public static Color InputControlFocusBackground => GetColor(White);

        /// <summary>
        /// Gets the input control focus foreground color.
        /// </summary>
        /// <value>
        /// The input control focus foreground color.
        /// </value>
        public static Color InputControlFocusForeground => GetColor(Black);

        /// <summary>
        /// Gets the input control disabled border color.
        /// </summary>
        /// <value>
        /// The input control disabled border color.
        /// </value>
        public static Color InputControlDisabledBorder => GetColor(LightGray);

        /// <summary>
        /// Gets the input control disabled background color.
        /// </summary>
        /// <value>
        /// The input control disabled background color.
        /// </value>
        public static Color InputControlDisabledBackground => GetColor(White);

        /// <summary>
        /// Gets the input control disabled foreground color.
        /// </summary>
        /// <value>
        /// The input control disabled foreground color.
        /// </value>
        public static Color InputControlDisabledForeground => GetColor(Black);

        private static Color GetColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Color cannot be null or white space.", nameof(color));
            }

            return (Color)ColorConverter.ConvertFromString(color);
        }
    }
}
