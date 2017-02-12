// <copyright file="ConnectionSettingsExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test.Extensions
{
    using System;
    using TeamCity.Extensions;
    using Xunit;

    public class ConnectionSettingsExtensionsTest
    {
        [Fact]
        public void CanGetBaseUriForHttpAuth()
        {
            var settings = new ConnectionSettings
            {
                AuthenticationType = AuthenticationType.HttpAuth,
                Url = "http://teamcity.jetbrains.com"
            };

            var baseUri = settings.GetBaseUri();

            Assert.Equal("http://teamcity.jetbrains.com/httpAuth/app/rest/10.0/", baseUri.ToString(), StringComparer.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void CanGetBaseUriForGuestAuth()
        {
            var settings = new ConnectionSettings
            {
                AuthenticationType = AuthenticationType.GuestAuth,
                Url = "http://teamcity.jetbrains.com"
            };

            var baseUri = settings.GetBaseUri();

            Assert.Equal("http://teamcity.jetbrains.com/guestAuth/app/rest/10.0/", baseUri.ToString(), StringComparer.InvariantCultureIgnoreCase);
        }

        [Fact]
        public void CanGetBaseUriForServerWithProtocolAndPort()
        {
            var settings = new ConnectionSettings
            {
                AuthenticationType = AuthenticationType.HttpAuth,
                Url = "https://teamcity.jetbrains.com:80"
            };

            var baseUri = settings.GetBaseUri();

            Assert.Equal("https://teamcity.jetbrains.com:80/httpAuth/app/rest/10.0/", baseUri.ToString(), StringComparer.InvariantCultureIgnoreCase);
        }
    }
}