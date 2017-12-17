// <copyright file="AppWindow.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// The <see cref="AppWindow" /> class.
    /// </summary>
    public abstract class AppWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppWindow" /> class.
        /// </summary>
        protected AppWindow()
        {
            WeakEventManager<AppWindow, EventArgs>.AddHandler(this, nameof(SourceInitialized), (sender, e) =>
            {
                var hWnd = new WindowInteropHelper(this).Handle;

                if (hWnd == IntPtr.Zero)
                {
                    return;
                }

                DisableMaximizeButton(hWnd);
            });
        }

        private static void DisableMaximizeButton(IntPtr hWnd)
        {
            var currentStyle = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);

            NativeMethods.SetWindowLong(hWnd, NativeMethods.GWL_STYLE, currentStyle & ~NativeMethods.WS_MAXIMIZEBOX);
        }
    }
}
