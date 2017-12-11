// <copyright file="DataProtector.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Security.Cryptography;

    /// <summary>
    /// The <see cref="DataProtector" /> class.
    /// </summary>
    public class DataProtector : IDataProtector
    {
        /// <summary>
        /// Protects the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="entropy">The entropy.</param>
        /// <returns>The protected user data.</returns>
        public byte[] Protect(byte[] userData, byte[] entropy)
        {
            return ProtectedData.Protect(userData, entropy, DataProtectionScope.CurrentUser);
        }

        /// <summary>
        /// Unprotects the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <param name="entropy">The entropy.</param>
        /// <returns>The unprotected user data.</returns>
        public byte[] Unprotect(byte[] userData, byte[] entropy)
        {
            return ProtectedData.Unprotect(userData, entropy, DataProtectionScope.CurrentUser);
        }
    }
}
