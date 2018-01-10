// <copyright file="NativeMethods.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The <see cref="NativeMethods" /> class.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// The style index.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const int GWL_STYLE = -16;

        /// <summary>
        /// The maximize box index.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public const int WS_MAXIMIZEBOX = 0x10000;

        // ReSharper disable once InconsistentNaming
        public const uint MF_BYCOMMAND = 0x00000000;

        // ReSharper disable once InconsistentNaming
        public const uint MF_GREYED = 0x00000001;

        // ReSharper disable once InconsistentNaming
        public const uint SC_CLOSE = 0xF060;

        /// <summary>
        /// Gets information about the specified window.
        /// </summary>
        /// <param name="hWnd">The window.</param>
        /// <param name="nIndex">The index of the information to get.</param>
        /// <returns>
        /// Information about the specified window.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Sets information about the specified window.
        /// </summary>
        /// <param name="hWnd">The window.</param>
        /// <param name="nIndex">The index of the information to set.</param>
        /// <param name="dwNewLong">The information to set.</param>
        /// <returns>
        /// The information set.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Enables, disables, or greys the specified menu item.
        /// </summary>
        /// <param name="hMenu">The menu.</param>
        /// <param name="uIDEnableItem">The menu item to enable, disable or grey.</param>
        /// <param name="uEnable">To enable, disable, or grey the menu item.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]

        // ReSharper disable once InconsistentNaming
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        /// <summary>
        /// Gets the system menu.
        /// </summary>
        /// <param name="hWnd">The window.</param>
        /// <param name="bRevert">The action to be taken.</param>
        /// <returns>The system menu.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
    }
}
