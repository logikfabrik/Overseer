// <copyright file="AppView.xaml.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Views.Windows
{
    using System;
    using System.Windows;
    using System.Windows.Forms;
    using EnsureThat;
    using ViewModels;

    /// <summary>
    /// The <see cref="AppView" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
#pragma warning disable S110 // Inheritance tree of classes should not be too deep
    public partial class AppView
#pragma warning restore S110 // Inheritance tree of classes should not be too deep
    {
        private readonly IApp _application;
        private readonly NotifyIcon _notifyIcon;
        private readonly MenuItem _showNotificationsMenuItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppView" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="appSettingsFactory">The app settings factory.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public AppView(IApp application, IAppSettingsFactory appSettingsFactory)
        {
            Ensure.That(application).IsNotNull();
            Ensure.That(appSettingsFactory).IsNotNull();

            InitializeComponent();

            _application = application;

            // ReSharper disable once PossibleNullReferenceException
#pragma warning disable S1075 // URIs should not be hardcoded
            var stream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/Logikfabrik.ico")).Stream;
#pragma warning restore S1075 // URIs should not be hardcoded

            using (stream)
            {
                _notifyIcon = new NotifyIcon
                {
                    Icon = new System.Drawing.Icon(stream)
                };
            }

            _notifyIcon.DoubleClick += ShowWindow;

            _showNotificationsMenuItem = new MenuItem(
                Properties.Resources.App_MenuItem_ShowNotifications,
                (sender, args) =>
                {
                    if (((MenuItem)sender).Checked)
                    {
                        HideNotifications();
                    }
                    else
                    {
                        ShowNotifications();
                    }
                })
            { Checked = appSettingsFactory.Create().ShowNotifications };

            var contextMenu = new ContextMenu(new[]
            {
                _showNotificationsMenuItem,
                new MenuItem("-"),
                new MenuItem(Properties.Resources.App_MenuItem_Open, ShowWindow)
            });

            _notifyIcon.ContextMenu = contextMenu;
        }

        /// <inheritdoc />
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Minimized)
            {
                HideWindow();
            }
        }

        /// <inheritdoc />
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _application.Shutdown();
        }

        private void HideWindow()
        {
            _notifyIcon.Visible = true;

            WindowState = WindowState.Minimized;
            Visibility = Visibility.Hidden;

            SystemCommands.MinimizeWindow(this);
        }

        private void ShowWindow(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void ShowWindow()
        {
            _notifyIcon.Visible = false;

            WindowState = WindowState.Normal;
            Visibility = Visibility.Visible;

            SystemCommands.RestoreWindow(this);
        }

        private void HideNotifications(object sender, EventArgs e)
        {
            HideNotifications();
        }

        private void HideNotifications()
        {
            var viewModel = (AppViewModel)DataContext;

            viewModel.HideNotifications();

            _showNotificationsMenuItem.Checked = false;
        }

        private void ShowNotifications(object sender, EventArgs e)
        {
            ShowNotifications();
        }

        private void ShowNotifications()
        {
            var viewModel = (AppViewModel)DataContext;

            viewModel.ShowNotifications();

            _showNotificationsMenuItem.Checked = true;
        }
    }
}