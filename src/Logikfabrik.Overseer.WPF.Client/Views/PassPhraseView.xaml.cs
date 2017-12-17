// <copyright file="PassPhraseView.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// The <see cref="PassPhraseView" /> class.
    /// </summary>
    public partial class PassPhraseView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassPhraseView" /> class.
        /// </summary>
        public PassPhraseView()
        {
            InitializeComponent();

            WeakEventManager<AppWindow, EventArgs>.AddHandler(this, nameof(SourceInitialized), (sender, e) =>
            {
                var hWnd = new WindowInteropHelper(this).Handle;

                if (hWnd == IntPtr.Zero)
                {
                    return;
                }

                DisableMinimizeButton(hWnd);
            });
        }

        private static void DisableMinimizeButton(IntPtr hWnd)
        {
            var currentStyle = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_STYLE);

            NativeMethods.SetWindowLong(hWnd, NativeMethods.GWL_STYLE, currentStyle & ~NativeMethods.WS_MINIMIZEBOX);
        }
    }
}
