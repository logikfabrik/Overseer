// <copyright file="NativeMethods.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The <see cref="NativeMethods" /> class.
    /// </summary>
    /// <remarks>
    /// Based on SO https://stackoverflow.com/a/1434577, answered by mark, https://stackoverflow.com/users/16363/mark.
    /// </remarks>
    internal static class NativeMethods
    {
        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window.
        /// </summary>
        /// <param name="hWnd">The window.</param>
        /// <param name="lpRect">A pointer to a <see cref="Rect" /> structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <returns><c>true</c> if the function succeeds; otherwise, <c>false</c>.</returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        /// <summary>
        /// The <see cref="Rect" /> structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
        /// </summary>
        public struct Rect
        {
            /// <summary>
            /// The x-coordinate of the upper-left corner of a rectangle.
            /// </summary>
            public int Left;

            /// <summary>
            /// The y-coordinate of the upper-left corner of a rectangle.
            /// </summary>
            public int Top;

            /// <summary>
            /// The x-coordinate of the lower-right corner of a rectangle.
            /// </summary>
            public int Right;

            /// <summary>
            /// The y-coordinate of the lower-right corner of a rectangle.
            /// </summary>
            public int Bottom;
        }
    }
}
