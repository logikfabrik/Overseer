// <copyright file="BuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
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
            Build = buildViewModelFactory.Create(project.Name, build.Id, build.Branch, build.VersionNumber(), build.RequestedBy, build.Changes, build.Status, build.StartTime, build.EndTime, build.RunTime(), build.WebUrl);

            StartClosing();
        }

        /// <summary>
        /// Occurs if closing.
        /// </summary>
        public event EventHandler<EventArgs> Closing;

        /// <inheritdoc />
        public IBuildViewModel Build { get; }

        /// <inheritdoc />
        public void KeepOpen()
        {
            _dispatcher?.Stop();
        }

        /// <inheritdoc />
        public void StartClosing()
        {
            const int showForSeconds = 10;

            _dispatcher?.Stop();

            _dispatcher = new DispatcherTimer(TimeSpan.FromSeconds(showForSeconds), DispatcherPriority.Normal, (sender, e) => { Close(); }, _application.Dispatcher);

            _dispatcher.Start();
        }

        /// <inheritdoc />
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