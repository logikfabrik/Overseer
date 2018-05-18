// <copyright file="ExceptionExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Extensions
{
    using System;
    using Overseer.Extensions;
    using Shouldly;
    using Xunit;

    public class ExceptionExtensionsTest
    {
        [Fact]
        public void GetsInnerExceptionWhenInnerExceptionExists()
        {
            var innerException = new Exception();
            var exception = new Exception(null, innerException);

            exception.InnerException().ShouldBe(innerException);
        }

        [Fact]
        public void GetsExceptionWhenInnerExceptionDoesNotExist()
        {
            var exception = new Exception();

            exception.InnerException().ShouldBe(exception);
        }
    }
}
