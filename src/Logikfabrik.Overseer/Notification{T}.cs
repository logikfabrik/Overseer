// <copyright file="Notification{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="Notification{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The payload type.</typeparam>
    public class Notification<T>
        where T : class
    {
        private Notification(NotificationType type, T payload)
        {
            Ensure.That(payload).IsNotNull();

            Type = type;
            Payload = payload;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public NotificationType Type { get; }

        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public T Payload { get; }

        /// <summary>
        /// Creates notifications using the specified type and payload.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>Notifications.</returns>
        public static Notification<T>[] Create(NotificationType type, IEnumerable<T> payload)
        {
            Ensure.That(payload).IsNotNull();

            return payload.Select(p => new Notification<T>(type, p)).ToArray();
        }

        /// <summary>
        /// Creates a notification using the specified type and payload.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>A notification.</returns>
        public static Notification<T> Create(NotificationType type, T payload)
        {
            Ensure.That(payload).IsNotNull();

            return new Notification<T>(type, payload);
        }

        /// <summary>
        /// Gets payloads.
        /// </summary>
        /// <param name="notifications">The notifications.</param>
        /// <param name="type">The type.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Payloads.</returns>
        public static IEnumerable<T> GetPayloads(
            IEnumerable<Notification<T>> notifications,
            NotificationType type,
            Predicate<T> predicate)
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
