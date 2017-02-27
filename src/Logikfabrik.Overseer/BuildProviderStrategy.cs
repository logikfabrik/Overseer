﻿// <copyright file="BuildProviderStrategy.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderStrategy" /> class.
    /// </summary>
    public class BuildProviderStrategy : IBuildProviderStrategy
    {
        private readonly Lazy<IEnumerable<IBuildProviderFactory>> _factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProviderStrategy" /> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        public BuildProviderStrategy(Lazy<IEnumerable<IBuildProviderFactory>> factories)
        {
            Ensure.That(factories).IsNotNull();

            _factories = factories;
        }

        /// <summary>
        /// Creates a build provider.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A build provider.
        /// </returns>
        public IBuildProvider Create(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            var type = settings.ProviderType;

            var factory = _factories.Value.Single(f => f.AppliesTo == type);

            return factory.Create(settings);
        }
    }
}
