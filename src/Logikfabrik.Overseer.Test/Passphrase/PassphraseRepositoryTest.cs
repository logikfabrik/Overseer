// <copyright file="PassphraseRepositoryTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Passphrase
{
    using System;
    using Moq;
    using Moq.AutoMock;
    using Overseer.IO.Registry;
    using Overseer.Passphrase;
    using Overseer.Security;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class PassphraseRepositoryTest
    {
        [Theory]
        [AutoData]
        public void CanWriteHash(string passphrase)
        {
            var mocker = new AutoMocker();

            var passphraseRepository = mocker.CreateInstance<PassphraseRepository>();

            var passphraseHash = HashUtility.GetHash(passphrase, HashUtility.GetSalt(16), 32);

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Protect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] userData, byte[] entropy) => userData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            passphraseRepository.WriteHash(passphraseHash);

            registryStoreMock.Verify(m => m.Write(PassphraseRepository.KeyName, It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public void CanReadHash(string passphrase)
        {
            var mocker = new AutoMocker();

            var passphraseRepository = mocker.CreateInstance<PassphraseRepository>();

            var dataProtectorMock = mocker.GetMock<IDataProtector>();

            dataProtectorMock.Setup(m => m.Unprotect(It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns((byte[] encryptedData, byte[] entropy) => encryptedData);

            var registryStoreMock = mocker.GetMock<IRegistryStore>();

            registryStoreMock.Setup(m => m.Read(PassphraseRepository.KeyName)).Returns(Convert.ToBase64String(HashUtility.GetHash(passphrase, HashUtility.GetSalt(16), 32)));

            var passphraseHash = passphraseRepository.ReadHash();

            passphraseHash.ShouldNotBeEmpty();
        }
    }
}
