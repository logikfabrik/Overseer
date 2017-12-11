// <copyright file="NotificationType.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    /// <summary>
    /// The <see cref="NotificationType" /> enumeration.
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// The payload was added.
        /// </summary>
        Added,

        /// <summary>
        /// The payload was updated.
        /// </summary>
        Updated,

        /// <summary>
        /// The payload was removed.
        /// </summary>
        Removed
    }
}
