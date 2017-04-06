// <copyright file="CacheManagerProvider.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Providers.Caching
{
    using System;
    using CacheManager.Core;
    using EnsureThat;
    using Ninject.Activation;

    /// <summary>
    /// The <see cref="CacheManagerProvider" /> class.
    /// </summary>
    public class CacheManagerProvider : Provider<ICacheManager<object>>
    {
        private readonly IAppSettingsFactory _appSettingsFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheManagerProvider" /> class.
        /// </summary>
        /// <param name="appSettingsFactory">The app settings factory.</param>
        public CacheManagerProvider(IAppSettingsFactory appSettingsFactory)
        {
            Ensure.That(appSettingsFactory).IsNotNull();

            _appSettingsFactory = appSettingsFactory;
        }

        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        protected override ICacheManager<object> CreateInstance(IContext context)
        {
            return CacheFactory.Build(settings => settings.WithUpdateMode(CacheUpdateMode.Up)
                .WithSystemRuntimeCacheHandle()
                .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromSeconds(_appSettingsFactory.Create().Expiration)));
        }
    }
}
