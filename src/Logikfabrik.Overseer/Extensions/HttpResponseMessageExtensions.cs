// <copyright file="HttpResponseMessageExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    using System.Net.Http;

    /// <summary>
    /// The <see cref="HttpResponseMessageExtensions" /> class.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Throws <see cref="HttpException" /> if unsuccessful.
        /// </summary>
        /// <param name="httpResponseMessage">The HTTP response message.</param>
        public static void ThrowIfUnsuccessful(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return;
            }

            throw new HttpException(httpResponseMessage.StatusCode);
        }
    }
}
