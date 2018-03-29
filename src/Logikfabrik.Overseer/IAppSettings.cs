// <copyright file="IAppSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    /// <summary>
    /// The <see cref="IAppSettings" /> interface.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Gets or sets the culture name.
        /// </summary>
        /// <value>
        /// The culture name.
        /// </value>
        string CultureName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show notifications.
        /// </summary>
        /// <value>
        ///   <c>true</c> if to show notifications; otherwise, <c>false</c>.
        /// </value>
        bool ShowNotifications { get; set; }

        bool ShowNotificationsForInProgressBuilds { get; set; }

        bool ShowNotificationsForFailedBuilds { get; set; }

        bool ShowNotificationsForSucceededBuilds { get; set; }

        bool ShowNotificationsForStoppedBuilds { get; set; }

        /// <summary>
        /// Stores the current app settings.
        /// </summary>
        void Save();
    }
}