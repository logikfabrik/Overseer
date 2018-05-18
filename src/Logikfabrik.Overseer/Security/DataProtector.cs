// <copyright file="DataProtector.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Security
{
    using System.Security.Cryptography;

    /// <summary>
    /// The <see cref="DataProtector" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class DataProtector : IDataProtector
    {
        /// <inheritdoc />
        public byte[] Protect(byte[] userData, byte[] entropy)
        {
            return ProtectedData.Protect(userData, entropy, DataProtectionScope.CurrentUser);
        }

        /// <inheritdoc />
        public byte[] Unprotect(byte[] userData, byte[] entropy)
        {
            return ProtectedData.Unprotect(userData, entropy, DataProtectionScope.CurrentUser);
        }
    }
}
