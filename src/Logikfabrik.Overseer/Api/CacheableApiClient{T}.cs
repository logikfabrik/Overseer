// <copyright file="CacheableApiClient{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Api
{
    using EnsureThat;
    using Settings;

    public abstract class CacheableApiClient<T> : ICacheableApiClient
        where T : ConnectionSettings
    {
        private readonly T _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheableApiClient{T}" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected CacheableApiClient(T settings)
        {
            Ensure.That(settings).IsNotNull();

            _settings = settings;
        }

        /// <summary>
        /// Gets the cache base key.
        /// </summary>
        /// <returns>The cache base key.</returns>
        public string GetCacheBaseKey()
        {
            return $"{GetType().FullName}({_settings.Signature()})";
        }
    }
}
