// <copyright file="BuildNotificationManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Caliburn.Micro;
    using EnsureThat;
    using Overseer.Extensions;
    using ViewModels;
    using ViewModels.Factories;

    /// <summary>
    /// The <see cref="BuildNotificationManager" /> class.
    /// </summary>
    public class BuildNotificationManager : NotificationManager<BuildNotificationViewModel>, IBuildNotificationManager
    {
        private readonly IBuildNotificationViewModelFactory _buildNotificationFactory;
        private readonly Lazy<DateTime> _appStartTime = new Lazy<DateTime>(() => Process.GetCurrentProcess().StartTime.ToUniversalTime());
        private readonly HashSet<string> _finishedBuilds = new HashSet<string>();
        private readonly HashSet<string> _buildsInProgress = new HashSet<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildNotificationManager" /> class.
        /// </summary>
        /// <param name="windowManager">The window manager.</param>
        /// <param name="displaySetting">The display setting.</param>
        /// <param name="buildNotificationFactory">The build notification factory.</param>
        public BuildNotificationManager(IWindowManager windowManager, IDisplaySetting displaySetting, IBuildNotificationViewModelFactory buildNotificationFactory)
            : base(windowManager, displaySetting)
        {
            Ensure.That(buildNotificationFactory).IsNotNull();

            _buildNotificationFactory = buildNotificationFactory;
        }

        /// <inheritdoc/>
        public void ShowNotification(IProject project, IBuild build)
        {
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            if (!ShouldShowNotification(project, build))
            {
                return;
            }

            var viewModel = _buildNotificationFactory.Create(project, build);

            ShowNotification(viewModel);
        }

        private bool ShouldShowNotification(IProject project, IBuild build)
        {
            // Only show notifications for builds running after app start time.
            if (build.EndTime <= _appStartTime.Value)
            {
                return false;
            }

            var id = string.Concat(project.Id, build.Id);

            if (build.IsInProgress())
            {
                if (_buildsInProgress.Contains(id))
                {
                    return false;
                }

                _buildsInProgress.Add(id);

                return true;
            }

            // ReSharper disable once InvertIf
            if (build.IsFinished())
            {
                if (_finishedBuilds.Contains(id))
                {
                    return false;
                }

                _finishedBuilds.Add(id);

                return true;
            }

            return false;
        }
    }
}
