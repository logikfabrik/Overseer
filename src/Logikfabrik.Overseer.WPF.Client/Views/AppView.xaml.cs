// <copyright file="AppView.xaml.cs" company="Logikfabrik">
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
    public partial class AppView
    {
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

        private void DisableMaximizeButton()
        {
            var handle = new WindowInteropHelper(this).Handle;

            if (handle == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.SetWindowLong(handle, NativeMethods.GWL_STYLE, NativeMethods.GetWindowLong(handle, NativeMethods.GWL_STYLE) & ~NativeMethods.WS_MAXIMIZEBOX);
        }
    }
}