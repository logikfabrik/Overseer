// <copyright file="NotificationUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="NotificationUtility" /> class.
    /// </summary>
    public static class NotificationUtility
    {
        /// <summary>
        /// Gets payloads.
        /// </summary>
        /// <typeparam name="T">The payload type.</typeparam>
        /// <param name="notifications">The notifications.</param>
        /// <param name="type">The type.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Payloads.</returns>
        public static IEnumerable<T> GetPayloads<T>(IEnumerable<Notification<T>> notifications, NotificationType type, Predicate<T> predicate)
            where T : class
        {
            Ensure.That(notifications).IsNotNull();
            Ensure.That(() => predicate).IsNotNull();

            return notifications
                .Where(notification => notification.Type == type)
                .Where(notification => predicate(notification.Payload))
                .Select(notification => notification.Payload);
        }
    }
}
