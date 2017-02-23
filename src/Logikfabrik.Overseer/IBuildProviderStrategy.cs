// <copyright file="IBuildProviderStrategy.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using Settings;

    /// <summary>
    /// The <see cref="IBuildProviderStrategy" /> interface.
    /// </summary>
    public interface IBuildProviderStrategy
    {
        /// <summary>
        /// Creates a build provider.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A build provider.
        /// </returns>
        IBuildProvider Create(ConnectionSettings settings);
    }
}
