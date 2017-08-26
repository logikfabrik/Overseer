// <copyright file="CacheInterceptor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Caching
{
    using System;
    using CacheManager.Core;
    using EnsureThat;
    using Logging;
    using Ninject.Extensions.Interception;
    using Ninject.Extensions.Interception.Request;

    /// <summary>
    /// The <see cref="CacheInterceptor" /> class.
    /// </summary>
    public class CacheInterceptor : IInterceptor
    {
        private readonly ILogService _logService;
        private readonly ICacheManager<object> _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheInterceptor" /> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public CacheInterceptor(ILogService logService, ICacheManager<object> cacheManager)
        {
            Ensure.That(logService).IsNotNull();
            Ensure.That(cacheManager).IsNotNull();

            _logService = logService;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            var request = invocation.Request;

            if (request.Method.ReturnType == typeof(void))
            {
                invocation.Proceed();

                return;
            }

            var key = GetKey(request);

            var proceeded = false;

            Func<string, object> f = cacheKey =>
            {
                invocation.Proceed();

                proceeded = true;

                return invocation.ReturnValue;
            };

            if (string.IsNullOrWhiteSpace(key))
            {
                f(null);

                return;
            }

            _logService.Log<CacheInterceptor>(new LogEntry(LogEntryType.Debug, $"Cache {(_cacheManager.Exists(key) ? "hit" : "miss")} for key '{key}'."));

            var returnValue = _cacheManager.GetOrAdd(key, f);

            if (proceeded)
            {
                return;
            }

            invocation.ReturnValue = returnValue;
        }

        private static string GetKey(IProxyRequest request)
        {
            var baseKey = GetBaseKey(request.Target);

            if (string.IsNullOrWhiteSpace(baseKey))
            {
                return null;
            }

            var key = string.Concat(baseKey, request.Method.Name, string.Join(null, request.Arguments));

            return GetHash(key);
        }

        private static string GetBaseKey(object target)
        {
            var cacheable = target as ICacheable;

            return cacheable?.GetCacheBaseKey();
        }

        private static string GetHash(string key)
        {
            return key.GetHashCode().ToString();
        }
    }
}