// <copyright file="IBuildProviderFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using Settings;

    /// <summary>
    /// The <see cref="IBuildProviderFactory" /> interface.
    /// </summary>
    public interface IBuildProviderFactory
    {
        /// <summary>
        /// Gets the type this factory applies to.
        /// </summary>
        /// <value>
        /// The type this factory applies to.
        /// </value>
        Type AppliesTo { get; }

        /// <summary>
        /// Creates a <see cref="IBuildProvider" />.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>A <see cref="IBuildProvider" />.</returns>
        IBuildProvider Create(ConnectionSettings settings);
    }
}