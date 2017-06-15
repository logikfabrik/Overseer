// <copyright file="BaseUriUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test
{
    using System;
    using Xunit;

    public class BaseUriUtilityTest
    {
        [Fact]
        public void CanGetBaseUriForHttpAuth()
        {
            var baseUri = BaseUriUtility.GetBaseUri("http://teamcity.jetbrains.com", "10.0", AuthenticationType.HttpAuth);

            Assert.Equal("http://teamcity.jetbrains.com/httpAuth/app/rest/10.0/", baseUri.ToString());
        }

        [Fact]
        public void CanGetBaseUriForGuestAuth()
        {
            var baseUri = BaseUriUtility.GetBaseUri("http://teamcity.jetbrains.com", "10.0", AuthenticationType.GuestAuth);

            Assert.Equal("http://teamcity.jetbrains.com/guestAuth/app/rest/10.0/", baseUri.ToString());
        }

        [Fact]
        public void CanGetBaseUriForServerWithProtocolAndPort()
        {
            var baseUri = BaseUriUtility.GetBaseUri("https://teamcity.jetbrains.com:80", "10.0", AuthenticationType.HttpAuth);

            Assert.Equal("https://teamcity.jetbrains.com:80/httpAuth/app/rest/10.0/", baseUri.ToString());
        }

        [Fact]
        public void CanTryAndFailToGetBaseUri()
        {
            Uri result;

            Assert.False(BaseUriUtility.TryGetBaseUri("ftp://teamcity.jetbrains.com", "9.1", AuthenticationType.HttpAuth, out result));
            Assert.Null(result);
        }

        [Fact]
        public void CanTryAndSucceedToGetBaseUri()
        {
            Uri result;

            Assert.True(BaseUriUtility.TryGetBaseUri("http://teamcity.jetbrains.com", "9.1", AuthenticationType.HttpAuth, out result));
            Assert.NotNull(result);
        }
    }
}