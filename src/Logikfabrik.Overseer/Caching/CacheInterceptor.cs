// <copyright file="CacheInterceptor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Caching
{
    using EnsureThat;
    using LazyCache;
    using Ninject.Extensions.Interception;
    using Ninject.Extensions.Interception.Request;

    /// <summary>
    /// The <see cref="CacheInterceptor" /> class.
    /// </summary>
    public class CacheInterceptor : IInterceptor
    {
        private readonly CachingService _cachingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheInterceptor" /> class.
        /// </summary>
        /// <param name="cachingService">The caching service.</param>
        public CacheInterceptor(CachingService cachingService)
        {
            Ensure.That(cachingService).IsNotNull();

            _cachingService = cachingService;
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            var request = invocation.Request;
            var method = request.Method;

            var key = $"{GetBaseKey(request)}.{method.Name}({string.Join(",", request.Arguments)})";

            var proceeded = false;

            var returnValue = _cachingService.GetOrAdd(key, () =>
            {
                invocation.Proceed();

                proceeded = true;

                return invocation.ReturnValue;
            });

            if (!proceeded)
            {
                invocation.ReturnValue = returnValue;
            }
        }

        private static string GetBaseKey(IProxyRequest request)
        {
            return request.Method.DeclaringType?.FullName;

            // TODO: Cache logic.

            //var cacheable = request.Target as ICacheable;

            //return cacheable == null ? request.Method.DeclaringType?.FullName : cacheable.GetCacheBaseKey();
        }
    }
}
