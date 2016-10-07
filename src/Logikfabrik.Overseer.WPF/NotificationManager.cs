// <copyright file="NotificationManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="NotificationManager" /> class.
    /// </summary>
    public class NotificationManager : INotificationManager
    {
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationManager" /> class.
        /// </summary>
        /// <param name="windowManager">The window manager.</param>
        public NotificationManager(IWindowManager windowManager)
        {
            Ensure.That(windowManager).IsNotNull();

            _windowManager = windowManager;
        }

        /// <summary>
        /// Shows the notification.
        /// </summary>
        /// <typeparam name="T">The view model type.</typeparam>
        /// <param name="viewModel">The view model.</param>
        public void ShowNotification<T>(T viewModel)
            where T : PropertyChangedBase
        {
            Ensure.That(viewModel).IsNotNull();

            Execute.OnUIThread(() =>
            {
                var settings = new Dictionary<string, object>
                {
                    { "Placement", PlacementMode.Custom },
                    {
                        "CustomPopupPlacementCallback", (CustomPopupPlacementCallback)((size, targetSize, offset) =>
                        {
                            var workArea = SystemParameters.WorkArea;

                            return new[]
                            {
                                new CustomPopupPlacement(new Point(workArea.Right - size.Width, workArea.Bottom - size.Height), PopupPrimaryAxis.Horizontal)
                            };
                        })
                    }
                };

                _windowManager.ShowPopup(viewModel, null, settings);
            });
        }
    }
}
