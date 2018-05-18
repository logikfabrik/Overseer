// <copyright file="IBuildTrackerSettingsFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    /// <summary>
    /// The <see cref="IBuildTrackerSettingsFactory" /> interface.
    /// </summary>
    public interface IBuildTrackerSettingsFactory
    {
        /// <summary>
        /// Creates an <see cref="IBuildTrackerSettings" /> instance.
        /// </summary>
        /// <returns>An <see cref="IBuildTrackerSettings" /> instance.</returns>
        IBuildTrackerSettings Create();
    }
}
