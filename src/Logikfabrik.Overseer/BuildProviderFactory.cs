// <copyright file="BuildProviderFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderFactory" /> class.
    /// </summary>
    public class BuildProviderFactory : IBuildProviderFactory
    {
        /// <summary>
        /// Creates a <see cref="IBuildProvider" />.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>A <see cref="IBuildProvider" />.</returns>
        public IBuildProvider Create(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            var constructor = settings.ProviderType.GetConstructor(new[] { settings.GetType() });

            // ReSharper disable once PossibleNullReferenceException
            var provider = (IBuildProvider)constructor.Invoke(new object[] { settings });

            return provider;
        }
    }
}
