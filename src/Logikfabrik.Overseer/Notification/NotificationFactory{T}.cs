// <copyright file="NotificationFactory{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Notification
{
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="NotificationFactory{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The payload type.</typeparam>
    public class NotificationFactory<T>
        where T : class
    {
        /// <summary>
        /// Creates notifications using the specified type and payloads.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="payloads">The payloads.</param>
        /// <returns>Notifications.</returns>
        public Notification<T>[] Create(NotificationType type, IEnumerable<T> payloads)
        {
            Ensure.That(payloads).IsNotNull();

            return payloads.Select(payload => new Notification<T>(type, payload)).ToArray();
        }

        /// <summary>
        /// Creates a notification using the specified type and payload.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>A notification.</returns>
        public Notification<T> Create(NotificationType type, T payload)
        {
            Ensure.That(payload).IsNotNull();

            return new Notification<T>(type, payload);
        }
    }
}
