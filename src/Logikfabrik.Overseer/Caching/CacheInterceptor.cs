// <copyright file="CacheInterceptor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Caching
{
    using System;
    using CacheManager.Core;
    using EnsureThat;
    using Ninject.Extensions.Interception;
    using Ninject.Extensions.Interception.Request;

    /// <summary>
    /// The <see cref="CacheInterceptor" /> class.
    /// </summary>
    public class CacheInterceptor : IInterceptor
    {
        private readonly ICacheManager<object> _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheInterceptor" /> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        public CacheInterceptor(ICacheManager<object> cacheManager)
        {
            Ensure.That(cacheManager).IsNotNull();

            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            var request = invocation.Request;
            var method = request.Method;

            if (method.ReturnType == typeof(void))
            {
                invocation.Proceed();

                return;
            }

            var key = $"{GetBaseKey(request)}.{method.Name}({string.Join(",", request.Arguments)})";

            var proceeded = false;

            Func<string, object> factory = cacheKey =>
            {
                invocation.Proceed();

                proceeded = true;

                return invocation.ReturnValue;
            };

            var returnValue = _cacheManager.GetOrAdd(key, factory);

            if (proceeded)
            {
                return;
            }

            invocation.ReturnValue = returnValue;
        }

        private static string GetBaseKey(IProxyRequest request)
        {
            var cacheable = request.Target as ICacheable;

            return cacheable == null ? request.Method.DeclaringType?.FullName : cacheable.GetCacheBaseKey();
        }
    }
}
