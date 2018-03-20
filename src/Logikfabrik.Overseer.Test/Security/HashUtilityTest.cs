// <copyright file="HashUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Security
{
    using Overseer.Security;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class HashUtilityTest
    {
        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        public void CanGetSalt(int size)
        {
            var salt = HashUtility.GetSalt(size);

            salt.Length.ShouldBe(size);
        }

        [Theory]
        [AutoData]
        public void CanGetHash(string password)
        {
            var salt = HashUtility.GetSalt(16);
            var hash = HashUtility.GetHash(password, salt, 32);

            hash.Length.ShouldBe(32);
        }
    }
}
