// <copyright file="ConnectionViewModelStrategy.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="ConnectionViewModelStrategy" /> class.
    /// </summary>
    public class ConnectionViewModelStrategy : IConnectionViewModelStrategy
    {
        private readonly IEnumerable<IConnectionViewModelFactory> _factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModelStrategy" /> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        public ConnectionViewModelStrategy(IEnumerable<IConnectionViewModelFactory> factories)
        {
            Ensure.That(factories).IsNotNull();

            _factories = factories;
        }

        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// A view model.
        /// </returns>
        public ConnectionViewModel Create(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            var type = settings.GetType();

            var factory = _factories.Single(f => f.AppliesTo == type);

            return factory.Create(settings);
        }
    }
}
