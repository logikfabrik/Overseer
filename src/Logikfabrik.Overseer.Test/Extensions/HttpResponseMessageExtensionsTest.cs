// <copyright file="HttpResponseMessageExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Extensions
{
    using System.Net;
    using System.Net.Http;
    using Overseer.Extensions;
    using Xunit;

    public class HttpResponseMessageExtensionsTest
    {
        [Fact]
        public void WillThrowIfUnsuccessful()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            Assert.Throws<HttpException>(() => httpResponseMessage.ThrowIfUnsuccessful());
        }

        [Fact]
        public void WillNotThrowIfNotUnsuccessful()
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            httpResponseMessage.ThrowIfUnsuccessful();
        }
    }
}
