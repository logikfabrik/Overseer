﻿// <copyright file="BuildNotificationManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using EnsureThat;
    using ViewModels;

    /// <summary>
    /// The <see cref="BuildNotificationManager" /> class.
    /// </summary>
    public class BuildNotificationManager : IBuildNotificationManager
    {
        private readonly INotificationManager _notificationManager;
        private readonly Lazy<DateTime> _appStartTime = new Lazy<DateTime>(() => Process.GetCurrentProcess().StartTime);
        private readonly HashSet<string> _finishedBuilds = new HashSet<string>();
        private readonly HashSet<string> _buildsInProgress = new HashSet<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildNotificationManager" /> class.
        /// </summary>
        /// <param name="notificationManager">The notification manager.</param>
        public BuildNotificationManager(INotificationManager notificationManager)
        {
            Ensure.That(notificationManager).IsNotNull();

            _notificationManager = notificationManager;
        }

        /// <summary>
        /// Shows a notification for the specified build, if a notification should be shown.
        /// </summary>
        /// <param name="build">The build.</param>
        public void ShowNotification(IBuild build)
        {
            Ensure.That(build).IsNotNull();

            if (!ShouldShowNotification(build))
            {
                return;
            }

            _notificationManager.ShowNotification(new BuildNotificationViewModel(build));
        }

        private bool ShouldShowNotification(IBuild build)
        {
            if (build.Finished <= _appStartTime.Value)
            {
                return false;
            }

            if (build.Finished.HasValue)
            {
                if (_finishedBuilds.Contains(build.Id))
                {
                    return false;
                }

                _finishedBuilds.Add(build.Id);
            }
            else
            {
                if (_buildsInProgress.Contains(build.Id))
                {
                    return false;
                }

                _buildsInProgress.Add(build.Id);
            }

            return true;
        }
    }
}