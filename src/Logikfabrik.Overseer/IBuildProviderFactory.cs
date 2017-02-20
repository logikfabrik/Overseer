// <copyright file="IBuildProviderFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using Settings;

    /// <summary>
    /// The <see cref="IBuildProviderFactory" /> interface.
    /// </summary>
    public interface IBuildProviderFactory
    {
        /// <summary>
        /// Creates a <see cref="IBuildProvider" />.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>A <see cref="IBuildProvider" />.</returns>
        IBuildProvider Create(ConnectionSettings settings);
    }
}