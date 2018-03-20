// <copyright file="PassphraseUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Passphrase
{
    using EnsureThat;
    using Security;

    /// <summary>
    /// The <see cref="PassphraseUtility" /> class.
    /// </summary>
    public static class PassphraseUtility
    {
        private static readonly byte[] Salt = { 155, 21, 6, 136, 63, 13, 179, 145, 46, 160, 245, 8, 208, 8, 50, 31 };

        /// <summary>
        /// Gets a hash.
        /// </summary>
        /// <param name="passphrase">The passphrase.</param>
        /// <returns>A hash.</returns>
        public static byte[] GetHash(string passphrase)
        {
            Ensure.That(passphrase).IsNotNullOrWhiteSpace();

            return HashUtility.GetHash(passphrase, Salt, 32);
        }
    }
}
