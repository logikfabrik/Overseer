// <copyright file="DisposableExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Extensions
{
    using System;
    using Moq;
    using Overseer.Extensions;
    using Xunit;

    public class DisposableExtensionsTest
    {
        [Fact]
        public void WillThrowIfDisposed()
        {
            var disposableMock = new Mock<IDisposable>();

            Assert.Throws<ObjectDisposedException>(() => disposableMock.Object.ThrowIfDisposed(true));
        }

        [Fact]
        public void WillNotThrowIfNotDisposed()
        {
            var disposableMock = new Mock<IDisposable>();

            disposableMock.Object.ThrowIfDisposed(false);
        }
    }
}