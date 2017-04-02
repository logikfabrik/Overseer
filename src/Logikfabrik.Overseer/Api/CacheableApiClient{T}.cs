// <copyright file="CacheableApiClient{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Api
{
    using System;
    using EnsureThat;
    using Settings;

    public abstract class CacheableApiClient<T> : ICacheableApiClient
        where T : ConnectionSettings
    {
        private readonly T _settings;

        protected CacheableApiClient(T settings)
        {
            Ensure.That(settings).IsNotNull();

            _settings = settings;
        }

        public string GetCacheBaseKey()
        {
            throw new NotImplementedException();
        }
    }
}
