// <copyright file="BuildNotificationViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
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
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        public BuildNotificationViewModel(IProject project, IBuild build)
        {
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            const int showForSeconds = 10;

            var dispatcher = new DispatcherTimer(TimeSpan.FromSeconds(showForSeconds), DispatcherPriority.Normal, (sender, args) => { Close(); }, Application.Current.Dispatcher);

            dispatcher.Start();

            BuildName = BuildMessageUtility.GetBuildName(project, build);
            BuildStatusMessage = BuildMessageUtility.GetBuildStatusMessage(build.Status, new Dictionary<string, string> { { "requested by", build.RequestedBy } });
            Status = build.Status;
        }

        /// <summary>
        /// Gets the build name.
        /// </summary>
        /// <value>
        /// The build name.
        /// </value>
        public string BuildName { get; }

        /// <summary>
        /// Gets the build status message.
        /// </summary>
        /// <value>
        /// The build status message.
        /// </value>
        public string BuildStatusMessage { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; }

        private void Close()
        {
            var popup = GetView() as Popup;

            if (popup == null)
            {
                return;
            }

            popup.IsOpen = false;
        }
    }
}