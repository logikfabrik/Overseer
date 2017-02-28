// <copyright file="ConnectionSettingsSignatureTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Settings
{
    using System;
    using Overseer.Settings;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    public class ConnectionSettingsSignatureTest
    {
        [Theory]
        [AutoData]
        public void AreEqual(Guid id, string name)
        {
            var settings1 = new ConnectionSettingsA { Id = id, Name = name };
            var settings2 = new ConnectionSettingsA { Id = id, Name = name };

            var signature1 = settings1.Signature();
            var signature2 = settings2.Signature();

            Assert.Equal(signature1, signature2);
        }

        [Theory]
        [AutoData]
        public void AreNotEqual(Guid id, string name1, string name2)
        {
            var settings1 = new ConnectionSettingsA { Id = id, Name = name1 };
            var settings2 = new ConnectionSettingsA { Id = id, Name = name2 };

            var signature1 = settings1.Signature();
            var signature2 = settings2.Signature();

            Assert.NotEqual(signature1, signature2);
        }
    }
}