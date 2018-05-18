// <copyright file="EditConnectionViewErrorLocalizerTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.Localization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using Overseer.Extensions;
    using Shouldly;
    using WPF.Localization;
    using Xunit;

    public class EditConnectionViewErrorLocalizerTest
    {
        [Theory]
        [ClassData(typeof(CanLocalizeClassData))]
        public void CanLocalize(Exception ex, string expected)
        {
            var errorMessage = EditConnectionViewErrorLocalizer.Localize(ex);

            errorMessage.ShouldBe(expected);
        }

        private class CanLocalizeClassData : IEnumerable<object[]>
        {
            private readonly IEnumerable<object[]> _data = new[]
            {
                new object[] { null, null },
                new object[] { new HttpException(HttpStatusCode.BadRequest), Properties.Resources.EditConnection_Error_HTTP4xx },
                new object[] { new HttpException(HttpStatusCode.InternalServerError), Properties.Resources.EditConnection_Error_HTTP5xx },
                new object[] { new SocketException(), Properties.Resources.EditConnection_Error_Network },
                new object[] { new Exception(), Properties.Resources.EditConnection_Error_Standard }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
