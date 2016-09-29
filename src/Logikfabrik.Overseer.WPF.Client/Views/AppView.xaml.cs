// <copyright file="AppView.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// The <see cref="AppView" /> class.
    /// </summary>
    public partial class AppView
    {
#pragma warning disable SA1310 // Field names must not contain underscore
        // ReSharper disable once InconsistentNaming
        private const int GWL_STYLE = -16;

        // ReSharper disable once InconsistentNaming
        private const int WS_MAXIMIZEBOX = 0x10000;
#pragma warning restore SA1310 // Field names must not contain underscore

        /// <summary>
        /// Initializes a new instance of the <see cref="AppView" /> class.
        /// </summary>
        public AppView()
        {
            InitializeComponent();

            WeakEventManager<AppView, EventArgs>.AddHandler(this, nameof(SourceInitialized), (sender, e) =>
            {
                DisableMaximizeButton();
            });
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void DisableMaximizeButton()
        {
            var handle = new WindowInteropHelper(this).Handle;

            if (handle == IntPtr.Zero)
            {
                return;
            }

            SetWindowLong(handle, GWL_STYLE, GetWindowLong(handle, GWL_STYLE) & ~WS_MAXIMIZEBOX);
        }
    }
}