// <copyright file="HttpException.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// The <see cref="HttpException" /> class.
    /// </summary>
    public class HttpException : HttpRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpException" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public HttpException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; }
    }
}
