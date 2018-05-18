// <copyright file="Notification{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Notification
{
    using EnsureThat;

    /// <summary>
    /// The <see cref="Notification{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The payload type.</typeparam>
    public class Notification<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Notification{T}" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="payload">The payload.</param>
        public Notification(NotificationType type, T payload)
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
    }
}