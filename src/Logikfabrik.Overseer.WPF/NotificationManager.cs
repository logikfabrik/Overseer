// <copyright file="NotificationManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Collections.Generic;
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
        /// <param name="placementCallback">The placement callback.</param>
        public void ShowNotification<T>(T viewModel, CustomPopupPlacementCallback placementCallback)
            where T : PropertyChangedBase
        {
            Ensure.That(viewModel).IsNotNull();
            Ensure.That(placementCallback).IsNotNull();

            Execute.OnUIThread(() =>
            {
                var settings = new Dictionary<string, object>
                {
                    { "Placement", PlacementMode.Custom },
                    { "CustomPopupPlacementCallback", placementCallback },
                    { "PopupAnimation", PopupAnimation.Fade }
                };

                _windowManager.ShowPopup(viewModel, null, settings);
            });
        }
    }
}
