// <copyright file="BuildProviderFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using EnsureThat;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderFactory" /> class.
    /// </summary>
    public static class BuildProviderFactory
    {
        /// <summary>
        /// Gets a build provider using the specified build provider settings.
        /// </summary>
        /// <param name="buildProviderSettings">The build provider settings.</param>
        /// <returns>A build provider.</returns>
        public static IBuildProvider GetBuildProvider(BuildProviderSettings buildProviderSettings)
        {
            Ensure.That(buildProviderSettings).IsNotNull();

            var buildProviderType = buildProviderSettings.GetBuildProviderType();

            var constructor = buildProviderType.GetConstructor(Type.EmptyTypes);

            // ReSharper disable once PossibleNullReferenceException
            var buildProvider = (IBuildProvider)constructor.Invoke(new object[] { });

            buildProvider.BuildProviderSettings = buildProviderSettings;

            return buildProvider;
        }
    }
}
