// <copyright file="IPassphraseRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Passphrase
{
    /// <summary>
    /// The <see cref="IPassphraseRepository" /> interface.
    /// </summary>
    public interface IPassphraseRepository
    {
        /// <summary>
        /// Gets a value indicating whether this instance has a passphrase hash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has a passphrase hash; otherwise, <c>false</c>.
        /// </value>
        bool HasHash { get; }

        /// <summary>
        /// Writes the passphrase hash.
        /// </summary>
        /// <param name="passphraseHash">The passphrase hash.</param>
        void WriteHash(byte[] passphraseHash);

        /// <summary>
        /// Reads the passphrase hash.
        /// </summary>
        /// <returns>The passphrase hash.</returns>
        byte[] ReadHash();
    }
}