// <copyright file="NotificationManager{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Interop;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="NotificationManager{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The <see cref="INotification" /> type.</typeparam>
    public abstract class NotificationManager<T>
        where T : class, INotification
    {
        private readonly IWindowManager _windowManager;
        private readonly Lazy<PopupPlacementHelper> _popupPlacementHelper;

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
            _popupPlacementHelper = new Lazy<PopupPlacementHelper>(() =>
            {
                var popupSize = default(Size);

                Execute.OnUIThread(() =>
                {
                    var view = (FrameworkElement)ViewLocator.LocateForModelType(typeof(T), null, null);

                    popupSize = new Size(view.Width, view.Height);
                });

                return new PopupPlacementHelper(displaySetting, popupSize);
            });
        }

        /// <summary>
        /// Shows the notification.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        protected void ShowNotification(T viewModel)
        {
            Ensure.That(viewModel).IsNotNull();

            var point = _popupPlacementHelper.Value.Hold();

            if (!point.HasValue)
            {
                // There's no available space for this popup.
                return;
            }

            viewModel.Closing += (sender, args) =>
            {
                var vm = (T)sender;

                var source = (HwndSource)PresentationSource.FromVisual(((Popup)vm.GetView()).Child);

                if (source == null)
                {
                    return;
                }

                var rect = default(NativeMethods.Rect);

                NativeMethods.GetWindowRect(source.Handle, ref rect);

                _popupPlacementHelper.Value.Release(new Point(rect.Left, rect.Top));
            };

            Execute.OnUIThread(() =>
            {
                CustomPopupPlacementCallback customPopupPlacementCallback = (popupSize, targetSize, offset) => new[]
                {
                    new CustomPopupPlacement(point.Value, PopupPrimaryAxis.Horizontal)
                };

                var settings = new Dictionary<string, object>
                {
                    { "Placement", PlacementMode.Custom },
                    { "CustomPopupPlacementCallback", customPopupPlacementCallback },
                    { "PopupAnimation", PopupAnimation.Fade }
                };

                _windowManager.ShowPopup(viewModel, null, settings);
            });
        }
    }
}
