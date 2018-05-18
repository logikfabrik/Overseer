// <copyright file="PassphraseRepository.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Passphrase
{
    using System;
    using System.Linq;
    using EnsureThat;
    using IO.Registry;
    using JetBrains.Annotations;
    using Security;

    /// <summary>
    /// The <see cref="PassphraseRepository" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class PassphraseRepository : IPassphraseRepository
    {
        /// <summary>
        /// The key name.
        /// </summary>
        public const string KeyName = "Passphrase";

        private readonly IDataProtector _dataProtector;
        private readonly IRegistryStore _registryStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="PassphraseRepository" /> class.
        /// </summary>
        /// <param name="dataProtector">The data protector.</param>
        /// <param name="registryStore">The registry store.</param>
        [UsedImplicitly]
        public PassphraseRepository(IDataProtector dataProtector, IRegistryStore registryStore)
        {
            Ensure.That(dataProtector).IsNotNull();
            Ensure.That(registryStore).IsNotNull();

            _dataProtector = dataProtector;
            _registryStore = registryStore;
        }

        /// <inheritdoc />
        public bool HasHash => ReadHash().Any();

        /// <inheritdoc />
        public void WriteHash(byte[] passphraseHash)
        {
            var entropy = HashUtility.GetSalt(16);

            var cipherValue = _dataProtector.Protect(passphraseHash, entropy);

            var registryValueBytes = new byte[16 + cipherValue.Length];

            Array.Copy(entropy, 0, registryValueBytes, 0, 16);
            Array.Copy(cipherValue, 0, registryValueBytes, 16, cipherValue.Length);

            var registryValue = Convert.ToBase64String(registryValueBytes);

            _registryStore.Write(KeyName, registryValue);
        }

        /// <inheritdoc />
        public byte[] ReadHash()
        {
            var registryValue = _registryStore.Read(KeyName);

            if (registryValue == null)
            {
                return new byte[] { };
            }

            var registryValueBytes = Convert.FromBase64String(registryValue);

            var entropy = new byte[16];

            var cipherValue = new byte[registryValueBytes.Length - 16];

            Array.Copy(registryValueBytes, 0, entropy, 0, 16);
            Array.Copy(registryValueBytes, 16, cipherValue, 0, cipherValue.Length);

            var passphraseHash = _dataProtector.Unprotect(cipherValue, entropy);

            return passphraseHash;
        }
    }
}
