// <copyright file="ConnectionSettingsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test
{
    using AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class ConnectionSettingsTest
    {
        [Fact]
        public void CanGetProviderType()
        {
            var settings = new ConnectionSettings();

            settings.ProviderType.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void CanGetUrl(string url)
        {
            var settings = new ConnectionSettings { Url = url };

            settings.Url.ShouldBe(url);
        }

        [Theory]
        [InlineData(AuthenticationType.GuestAuth)]
        [InlineData(AuthenticationType.HttpAuth)]
        public void CanGetAuthenticationType(AuthenticationType authenticationType)
        {
            var settings = new ConnectionSettings { AuthenticationType = authenticationType };

            settings.AuthenticationType.ShouldBe(authenticationType);
        }

        [Theory]
        [AutoData]
        public void CanGetVersion(string version)
        {
            var settings = new ConnectionSettings { Version = version };

            settings.Version.ShouldBe(version);
        }

        [Theory]
        [AutoData]
        public void CanGetUsername(string username)
        {
            var settings = new ConnectionSettings { Username = username };

            settings.Username.ShouldBe(username);
        }

        [Theory]
        [AutoData]
        public void CanGetPassword(string password)
        {
            var settings = new ConnectionSettings { Password = password };

            settings.Password.ShouldBe(password);
        }
    }
}
