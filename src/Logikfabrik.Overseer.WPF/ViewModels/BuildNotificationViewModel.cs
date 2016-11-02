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
    using Overseer.Extensions;

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

            BuildName = GetBuildName(project, build);
            Message = GetMessage(build);
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
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; }

        private static string GetBuildName(IProject project, IBuild build)
        {
            return $"{project.Name} {build.GetVersionNumber()} {(!string.IsNullOrWhiteSpace(build.Branch) ? $"({build.Branch})" : string.Empty)}";
        }

        private static string GetMessage(IBuild build)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (build.Status)
            {
                case BuildStatus.InProgress:
                    return $"Build requested by {build.RequestedBy} is in progress";

                case BuildStatus.Stopped:
                    return $"Build requested by {build.RequestedBy} was stopped";

                case BuildStatus.Succeeded:
                    return $"Build requested by {build.RequestedBy} succeeded";

                case BuildStatus.Failed:
                    return $"Build requested by {build.RequestedBy} failed";

                default:
                    return null;
            }
        }

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