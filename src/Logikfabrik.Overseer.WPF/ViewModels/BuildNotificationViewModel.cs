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

    /// <summary>
    /// The <see cref="BuildNotificationViewModel" /> class.
    /// </summary>
    public class BuildNotificationViewModel : ViewAware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildNotificationViewModel" /> class.
        /// </summary>
        /// <param name="build">The build.</param>
        public BuildNotificationViewModel(IBuild build)
        {
            Ensure.That(build).IsNotNull();

            BuildViewModel = new BuildViewModel(build);

            var dispatcher = new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.Normal, (sender, args) => { Close(); }, Application.Current.Dispatcher);

            dispatcher.Start();

            Notification = GetNotification(build);
        }

        /// <summary>
        /// Gets the build view model.
        /// </summary>
        /// <value>
        /// The build view model.
        /// </value>
        public BuildViewModel BuildViewModel { get; }

        /// <summary>
        /// Gets the notification.
        /// </summary>
        /// <value>
        /// The notification.
        /// </value>
        public string Notification { get; }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            var popup = GetView() as Popup;

            if (popup == null)
            {
                return;
            }

            popup.IsOpen = false;
        }

        private static string GetNotification(IBuild build)
        {
            // TODO: Use build status.

            if (!build.Finished.HasValue)
            {
                return $"Build requested by {build.RequestedBy} in progress";
            }

            return $"Build requested by {build.RequestedBy} succeeded";
        }
    }
}
