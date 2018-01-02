// <copyright file="HttpExceptionTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Extensions
{
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Formatters.Binary;
    using Overseer.Extensions;
    using Shouldly;
    using Xunit;

    public class HttpExceptionTest
    {
        [Fact]
        public void CanSerializeAndDeserialize()
        {
            var ex = new HttpException(HttpStatusCode.BadRequest);

            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, ex);

                stream.Seek(0, 0);

                ex = (HttpException)formatter.Deserialize(stream);
            }

            ex.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
