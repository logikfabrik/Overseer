// <copyright file="BuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Threading;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="BuildNotificationViewModel" /> class.
    /// </summary>
    public class BuildNotificationViewModel : ViewAware, IBuildNotificationViewModel
    {
        private readonly IApp _application;
        private readonly Uri _webUrl;
        private DispatcherTimer _dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildNotificationViewModel" /> class.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="buildViewModelFactory">The build view model factory.</param>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        public BuildNotificationViewModel(IApp application, IBuildViewModelFactory buildViewModelFactory, IProject project, IBuild build)
        {
            Ensure.That(application).IsNotNull();
            Ensure.That(buildViewModelFactory).IsNotNull();
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            _application = application;
            Build = buildViewModelFactory.Create(project.Name, build.Id, build.Branch, build.VersionNumber(), build.RequestedBy, build.Changes, build.Status, build.StartTime, build.EndTime, build.RunTime());
            _webUrl = build.WebUrl;

            StartClosing();
        }

        /// <summary>
        /// Occurs if closing.
        /// </summary>
        public event EventHandler<EventArgs> Closing;

        /// <summary>
        /// Gets the build.
        /// </summary>
        /// <value>
        /// The build.
        /// </value>
        public IBuildViewModel Build { get; }

        /// <summary>
        /// Opens the notification in the browser.
        /// </summary>
        public void ViewInBrowser()
        {
            if (_webUrl == null)
            {
                return;
            }

            Process.Start(new ProcessStartInfo(_webUrl.AbsoluteUri));
        }

        /// <summary>
        /// Keeps the notification open.
        /// </summary>
        public void KeepOpen()
        {
            _dispatcher?.Stop();
        }

        /// <summary>
        /// Starts closing the notification.
        /// </summary>
        public void StartClosing()
        {
            const int showForSeconds = 10;

            _dispatcher?.Stop();

            _dispatcher = new DispatcherTimer(TimeSpan.FromSeconds(showForSeconds), DispatcherPriority.Normal, (sender, e) => { Close(); }, _application.Dispatcher);

            _dispatcher.Start();
        }

        /// <summary>
        /// Closes the notification.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        public void Close(RoutedEventArgs e)
        {
            Close();

            e.Handled = true;
        }

        /// <summary>
        /// Raises the <see cref="Closing" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnClosing(EventArgs e)
        {
            Closing?.Invoke(this, e);
        }

        private void Close()
        {
            _dispatcher?.Stop();
            _dispatcher = null;

            var popup = GetView() as Popup;

            if (popup == null)
            {
                return;
            }

            OnClosing(EventArgs.Empty);

            popup.IsOpen = false;
        }
    }
}