// <copyright file="BaseUriUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test
{
    using System;
    using Shouldly;
    using Xunit;

    public class BaseUriUtilityTest
    {
        [Fact]
        public void WillThrowIfUnsupportedScheme()
        {
            Assert.Throws<NotSupportedException>(() => BaseUriUtility.GetBaseUri("ftp://teamcity.jetbrains.com", "10.0", AuthenticationType.HttpAuth));
        }

        [Fact]
        public void CanGetBaseUriForHttpAuth()
        {
            var baseUri = BaseUriUtility.GetBaseUri("http://teamcity.jetbrains.com", "10.0", AuthenticationType.HttpAuth);

            baseUri.ToString().ShouldBe("http://teamcity.jetbrains.com/httpAuth/app/rest/10.0/");
        }

        [Fact]
        public void CanGetBaseUriForGuestAuth()
        {
            var baseUri = BaseUriUtility.GetBaseUri("http://teamcity.jetbrains.com", "10.0", AuthenticationType.GuestAuth);

            baseUri.ToString().ShouldBe("http://teamcity.jetbrains.com/guestAuth/app/rest/10.0/");
        }

        [Fact]
        public void CanGetBaseUriForServerWithProtocolAndPort()
        {
            var baseUri = BaseUriUtility.GetBaseUri("https://teamcity.jetbrains.com:80", "10.0", AuthenticationType.HttpAuth);

            baseUri.ToString().ShouldBe("https://teamcity.jetbrains.com:80/httpAuth/app/rest/10.0/");
        }

        [Fact]
        public void CanTryAndFailToGetBaseUri()
        {
            Uri result;

            BaseUriUtility.TryGetBaseUri("ftp://teamcity.jetbrains.com", "9.1", AuthenticationType.HttpAuth, out result).ShouldBeFalse();
        }

        [Fact]
        public void CanTryAndSucceedToGetBaseUri()
        {
            Uri result;

            BaseUriUtility.TryGetBaseUri("http://teamcity.jetbrains.com", "9.1", AuthenticationType.HttpAuth, out result).ShouldBeTrue();
        }

        [Fact]
        public void CanTryAndFailToGetBaseUriForNullUrl()
        {
            Uri result;

            BaseUriUtility.TryGetBaseUri(null, "10.0", AuthenticationType.HttpAuth, out result).ShouldBeFalse();
        }

        [Fact]
        public void CanTryAndFailToGetBaseUriForNullVersion()
        {
            Uri result;

            BaseUriUtility.TryGetBaseUri("http://teamcity.jetbrains.com", null, AuthenticationType.HttpAuth, out result).ShouldBeFalse();
        }
    }
}