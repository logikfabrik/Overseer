// <copyright file="ExceptionExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Extensions
{
    using System;

    /// <summary>
    /// The <see cref="ExceptionExtensions" /> class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets the inner exception of the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The inner exception, or the specified exception.</returns>
        public static Exception InnerException(this Exception exception)
        {
            var ex = exception;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex;
        }
    }
}
