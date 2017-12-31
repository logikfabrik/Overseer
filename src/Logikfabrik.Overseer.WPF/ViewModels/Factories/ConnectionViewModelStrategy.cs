// <copyright file="ConnectionViewModelStrategy.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionViewModelStrategy" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ConnectionViewModelStrategy : IConnectionViewModelStrategy
    {
        private readonly Lazy<IEnumerable<IConnectionViewModelFactory>> _factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModelStrategy" /> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        public ConnectionViewModelStrategy(Lazy<IEnumerable<IConnectionViewModelFactory>> factories)
        {
            Ensure.That(factories).IsNotNull();

            _factories = factories;
        }

        /// <inheritdoc />
        public IConnectionViewModel Create(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            var type = settings.GetType();

            var factory = _factories.Value.Single(f => f.AppliesTo == type);

            return factory.Create(settings);
        }
    }
}
