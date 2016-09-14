// <copyright file="BuildProviderFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using Settings;

    /// <summary>
    /// The <see cref="BuildProviderFactory" /> class.
    /// </summary>
    public static class BuildProviderFactory
    {
        public static BuildProvider GetProvider(BuildProviderSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var providerType = settings.GetProviderType();

            var constructor = providerType.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
            {
                throw new Exception();
            }

            var provider = (BuildProvider)constructor.Invoke(new object[] { });

            provider.Settings = settings;

            return provider;
        }
    }
}
