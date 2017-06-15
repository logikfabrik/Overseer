// <copyright file="HashUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Settings
{
    using System.Security.Cryptography;
    using EnsureThat;

    /// <summary>
    /// The <see cref="HashUtility" /> class.
    /// </summary>
    public static class HashUtility
    {
        /// <summary>
        /// Gets a salt.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>A salt.</returns>
        public static byte[] GetSalt(int size)
        {
            Ensure.That(size % 16).Is(0);

            var salt = new byte[size];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        /// <summary>
        /// Gets a hash.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="size">The size.</param>
        /// <returns>A hash.</returns>
        public static byte[] GetHash(string passPhrase, byte[] salt, int size)
        {
            Ensure.That(passPhrase).IsNotNullOrWhiteSpace();
            Ensure.That(salt).IsNotNull();
            Ensure.That(size % 16).Is(0);

            // ReSharper disable once ArgumentsStyleLiteral
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, salt, iterations: 10000);

            return rfc2898DeriveBytes.GetBytes(size);
        }
    }
}
