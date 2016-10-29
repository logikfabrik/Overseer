// <copyright file="IBuildNotificationManager.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    /// <summary>
    /// The <see cref="IBuildNotificationManager" /> interface.
    /// </summary>
    public interface IBuildNotificationManager
    {
        /// <summary>
        /// Shows a notification for the specified build, if a notification should be shown.
        /// </summary>
        /// <param name="build">The build.</param>
        void ShowNotification(IBuild build);
    }
}
