// <copyright file="IDataProtector.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Security
{
    /// <summary>
    /// The <see cref="IDataProtector" /> interface.
    /// </summary>
    public interface IDataProtector
    {
        /// <summary>
        /// Protects the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="entropy">The entropy.</param>
        /// <returns>The protected user data.</returns>
        byte[] Protect(byte[] userData, byte[] entropy);

        /// <summary>
        /// Unprotects the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="entropy">The entropy.</param>
        /// <returns>The unprotected user data.</returns>
        byte[] Unprotect(byte[] userData, byte[] entropy);
    }
}