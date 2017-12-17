// <copyright file="AppView.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views
{
    using System;
    using System.Windows;
    using System.Windows.Forms;
    using EnsureThat;

    /// <summary>
    /// The <see cref="AppView" /> class.
    /// </summary>
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public partial class AppView
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        private readonly IApp _application;
        private readonly NotifyIcon _notifyIcon;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppView" /> class.
        /// </summary>
        public AppView(IApp application)
        {
            Ensure.That(application).IsNotNull();

            InitializeComponent();

            _application = application;

            var stream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/Overseer.ico"))?.Stream;

            if (stream == null)
            {
                return;
            }

            using (stream)
            {
                _notifyIcon = new NotifyIcon
                {
                    Icon = new System.Drawing.Icon(stream),

                };

                _notifyIcon.Click += (sender, args) =>
                {
                    ShowView();
                };
            }
        }

        /// <inheritdoc/>
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Minimized)
            {
                HideView();
            }
        }

        /// <inheritdoc/>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _application.Shutdown();
        }

        private void HideView()
        {
            _notifyIcon.Visible = true;

            WindowState = WindowState.Minimized;
            Visibility = Visibility.Hidden;

            SystemCommands.MinimizeWindow(this);
        }

        private void ShowView()
        {
            _notifyIcon.Visible = false;

            WindowState = WindowState.Normal;
            Visibility = Visibility.Visible;

            SystemCommands.RestoreWindow(this);
        }
    }
}