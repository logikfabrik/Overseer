// <copyright file="ViewConnectionViewModelStrategy.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using JetBrains.Annotations;
    using Settings;

    /// <summary>
    /// The <see cref="ViewConnectionViewModelStrategy" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewConnectionViewModelStrategy : IViewConnectionViewModelStrategy
    {
        private readonly Lazy<IEnumerable<IViewConnectionViewModelFactory>> _factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewConnectionViewModelStrategy" /> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        [UsedImplicitly]
        public ViewConnectionViewModelStrategy(Lazy<IEnumerable<IViewConnectionViewModelFactory>> factories)
        {
            Ensure.That(factories).IsNotNull();

            _factories = factories;
        }

        /// <inheritdoc />
        public IViewConnectionViewModel Create(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            var type = settings.GetType();

            var factory = _factories.Value.Single(f => f.AppliesTo == type);

            return factory.Create(settings);
        }
    }
}
