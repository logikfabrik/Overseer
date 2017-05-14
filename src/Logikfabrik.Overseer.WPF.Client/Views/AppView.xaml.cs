﻿// <copyright file="AppView.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// The <see cref="AppView" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public partial class AppView
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppView" /> class.
        /// </summary>
        public AppView()
        {
            InitializeComponent();

            WeakEventManager<AppView, EventArgs>.AddHandler(this, nameof(SourceInitialized), (sender, e) =>
            {
                var hWnd = new WindowInteropHelper(this).Handle;

                if (hWnd == IntPtr.Zero)
                {
                    return;
                }

                HideIcon(hWnd);
                DisableMaximizeButton(hWnd);
            });
        }

        private static void HideIcon(IntPtr hWnd)
        {
            NativeMethods.SetWindowLong(hWnd, NativeMethods.GWL_EXSTYLE, NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_EXSTYLE) | NativeMethods.WS_EX_DLGMODALFRAME);
        }

        private static void DisableMaximizeButton(IntPtr hWnd)
        {
            NativeMethods.SetWindowLong(hWnd, NativeMethods.GWL_STYLE, NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE) & ~NativeMethods.WS_MAXIMIZEBOX);
        }
    }
}