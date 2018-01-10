// <copyright file="WizardView.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views.Wizard
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// The <see cref="WizardView" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public partial class WizardView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WizardView" /> class.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public WizardView()
        {
            InitializeComponent();

            WeakEventManager<AppWindow, EventArgs>.AddHandler(this, nameof(SourceInitialized), (sender, e) =>
            {
                var hWnd = new WindowInteropHelper(this).Handle;

                if (hWnd == IntPtr.Zero)
                {
                    return;
                }

                DisableCloseButton(hWnd);
            });
        }

        private static void DisableCloseButton(IntPtr hWnd)
        {
            IntPtr hMenu = NativeMethods.GetSystemMenu(hWnd, false);

            if (hMenu != IntPtr.Zero)
            {
                NativeMethods.EnableMenuItem(hMenu, NativeMethods.SC_CLOSE, NativeMethods.MF_BYCOMMAND | NativeMethods.MF_GRAYED);
            }
        }
    }
}
