// <copyright file="BuildNotificationManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Interop;
    using EnsureThat;
    using Overseer.Extensions;
    using ViewModels;
    using ViewModels.Factories;

    /// <summary>
    /// The <see cref="BuildNotificationManager" /> class.
    /// </summary>
    public class BuildNotificationManager : IBuildNotificationManager
    {
        private readonly INotificationManager _notificationManager;
        private readonly IBuildNotificationViewModelFactory _buildNotificationFactory;
        private readonly Lazy<DateTime> _appStartTime = new Lazy<DateTime>(() => Process.GetCurrentProcess().StartTime.ToUniversalTime());
        private readonly HashSet<string> _finishedBuilds = new HashSet<string>();
        private readonly HashSet<string> _buildsInProgress = new HashSet<string>();
        private PopupPlacementHelper _popupPlacementHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildNotificationManager" /> class.
        /// </summary>
        /// <param name="notificationManager">The notification manager.</param>
        /// <param name="buildNotificationFactory">The build notification factory.</param>
        public BuildNotificationManager(INotificationManager notificationManager, IBuildNotificationViewModelFactory buildNotificationFactory)
        {
            Ensure.That(notificationManager).IsNotNull();
            Ensure.That(buildNotificationFactory).IsNotNull();

            _notificationManager = notificationManager;
            _buildNotificationFactory = buildNotificationFactory;
        }

        /// <summary>
        /// Shows a notification for the specified build, if a notification should be shown.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        public void ShowNotification(IProject project, IBuild build)
        {
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            if (!ShouldShowNotification(project, build))
            {
                return;
            }

            var model = _buildNotificationFactory.Create(project, build);

            model.Closing += BuildNotificationViewModelOnClosing;

            _notificationManager.ShowNotification(model, GetPopupPlacement);
        }

        private void BuildNotificationViewModelOnClosing(object sender, EventArgs eventArgs)
        {
            var model = (BuildNotificationViewModel)sender;

            var source = (HwndSource)PresentationSource.FromVisual(((Popup)model.GetView()).Child);

            var rect = default(NativeMethods.Rect);

            // ReSharper disable once PossibleNullReferenceException
            NativeMethods.GetWindowRect(source.Handle, ref rect);

            _popupPlacementHelper.Release(new Point(rect.Left, rect.Top));

            model.Closing -= BuildNotificationViewModelOnClosing;
        }

        private CustomPopupPlacement[] GetPopupPlacement(Size popupSize, Size targetSize, Point offset)
        {
            if (_popupPlacementHelper == null)
            {
                _popupPlacementHelper = new PopupPlacementHelper(() => SystemParameters.WorkArea, popupSize);
            }

            var point = _popupPlacementHelper.Hold();

            return new[]
            {
                new CustomPopupPlacement(point, PopupPrimaryAxis.Horizontal)
            };
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
