// <copyright file="HashUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using Overseer.Settings;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    public class HashUtilityTest
    {
        [Theory]
        [InlineAutoData(16)]
        [InlineAutoData(32)]
        public void CanGetSalt(int size)
        {
            var salt = HashUtility.GetSalt(size);

            Assert.Equal(size, salt.Length);
        }

        [Theory]
        [AutoData]
        public void CanGetHash(string passPhrase)
        {
            var salt = HashUtility.GetSalt(16);
            var hash = HashUtility.GetHash(passPhrase, salt, 32);

            Assert.Equal(32, hash.Length);
        }
    }
}
