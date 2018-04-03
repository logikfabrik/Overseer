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
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildNotificationManager : NotificationManager<ViewNotificationViewModel>, IBuildNotificationManager
    {
        private readonly IViewNotificationViewModelFactory _viewNotificationViewModelFactory;
        private readonly IAppSettingsFactory _appSettingsFactory;
        private readonly Lazy<DateTime> _appStartTime = new Lazy<DateTime>(() => Process.GetCurrentProcess().StartTime.ToUniversalTime());
        private readonly HashSet<Tuple<string, string>> _finishedBuilds = new HashSet<Tuple<string, string>>();
        private readonly HashSet<Tuple<string, string>> _buildsInProgress = new HashSet<Tuple<string, string>>();
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildNotificationManager" /> class.
        /// </summary>
        /// <param name="windowManager">The window manager.</param>
        /// <param name="displaySetting">The display setting.</param>
        /// <param name="viewNotificationViewModelFactory">The build notification factory.</param>
        /// <param name="appSettingsFactory">The app settings factory.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public BuildNotificationManager(IWindowManager windowManager, IDisplaySetting displaySetting, IViewNotificationViewModelFactory viewNotificationViewModelFactory, IAppSettingsFactory appSettingsFactory)
            : base(windowManager, displaySetting)
        {
            Ensure.That(viewNotificationViewModelFactory).IsNotNull();
            Ensure.That(appSettingsFactory).IsNotNull();

            _viewNotificationViewModelFactory = viewNotificationViewModelFactory;
            _appSettingsFactory = appSettingsFactory;
        }

        /// <inheritdoc />
        public void ShowNotification(IProject project, IBuild build)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            if (!ShouldShowNotification(project, build))
            {
                return;
            }

            var viewModel = _viewNotificationViewModelFactory.Create(project, build);

            ShowNotification(viewModel);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
        }

        private static bool ShouldShowNotification(IAppSettings settings, BuildStatus? status)
        {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (status == BuildStatus.InProgress && !settings.ShowNotificationsForInProgressBuilds)
            {
                return false;
            }

            if (status == BuildStatus.Failed && !settings.ShowNotificationsForFailedBuilds)
            {
                return false;
            }

            if (status == BuildStatus.Succeeded && !settings.ShowNotificationsForSucceededBuilds)
            {
                return false;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (status == BuildStatus.Stopped && !settings.ShowNotificationsForStoppedBuilds)
            {
                return false;
            }

            return true;
        }

        private bool ShouldShowNotification(IProject project, IBuild build)
        {
            // Only show notifications for builds running after app start time.
            if (build.EndTime <= _appStartTime.Value)
            {
                return false;
            }

            var settings = _appSettingsFactory.Create();

            if (!settings.ShowNotifications)
            {
                return false;
            }

            if (!ShouldShowNotification(settings, build.Status))
            {
                return false;
            }

            var id = new Tuple<string, string>(project.Id, build.Id);

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
