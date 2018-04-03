// <copyright file="NotificationManager{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using Caliburn.Micro;
    using EnsureThat;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="NotificationManager{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="INotification" /> type.</typeparam>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class NotificationManager<T> : IDisposable
        where T : class, INotification
    {
        private readonly IWindowManager _windowManager;
        private Lazy<NotificationGrid<T>> _notificationGrid;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationManager{T}" /> class.
        /// </summary>
        /// <param name="windowManager">The window manager.</param>
        /// <param name="displaySetting">The display setting.</param>
        protected NotificationManager(IWindowManager windowManager, IDisplaySetting displaySetting)
        {
            Ensure.That(windowManager).IsNotNull();
            Ensure.That(displaySetting).IsNotNull();

            _windowManager = windowManager;
            _notificationGrid = new Lazy<NotificationGrid<T>>(() =>
            {
                var popupSize = default(Size);

                Execute.OnUIThread(() =>
                {
                    var view = (FrameworkElement)ViewLocator.LocateForModelType(typeof(T), null, null);

                    popupSize = new Size(view.Width, view.Height);
                });

                return new NotificationGrid<T>(displaySetting, popupSize);
            });
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Shows the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        protected void ShowNotification(T notification)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(notification).IsNotNull();

            var cellScreenPoint = _notificationGrid.Value.HoldCell(notification);

            if (!cellScreenPoint.HasValue)
            {
                // There's no available cell for this notification.
                return;
            }

            notification.Closing += (sender, args) =>
            {
                var vm = (T)sender;

                _notificationGrid.Value.ReleaseCell(vm);
            };

            Execute.OnUIThread(() =>
            {
                CustomPopupPlacementCallback customPopupPlacementCallback = (popupSize, targetSize, offset) => new[]
                {
                    new CustomPopupPlacement(cellScreenPoint.Value, PopupPrimaryAxis.Horizontal)
                };

                var settings = new Dictionary<string, object>
                {
                    { "Placement", PlacementMode.Custom },
                    { "CustomPopupPlacementCallback", customPopupPlacementCallback },
                    { "PopupAnimation", PopupAnimation.Fade }
                };

                _windowManager.ShowPopup(notification, null, settings);
            });
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing && _notificationGrid != null)
            {
                if (_notificationGrid.IsValueCreated)
                {
                    _notificationGrid.Value.Dispose();
                }

                _notificationGrid = null;
            }

            _isDisposed = true;
        }
    }
}
