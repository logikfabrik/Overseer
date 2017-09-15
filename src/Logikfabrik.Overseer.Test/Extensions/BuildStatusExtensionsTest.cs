// <copyright file="BuildStatusExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Extensions
{
    using Overseer.Extensions;
    using Xunit;

    public class BuildStatusExtensionsTest
    {
        [Theory]
        [InlineData(BuildStatus.Failed, true)]
        [InlineData(BuildStatus.Succeeded, true)]
        [InlineData(BuildStatus.InProgress, false)]
        [InlineData(BuildStatus.Stopped, true)]
        [InlineData(BuildStatus.Queued, false)]
        [InlineData(null, false)]
        public void CanGetIsFinished(BuildStatus? status, bool expected)
        {
            Assert.Equal(expected, status.IsFinished());
        }

        [Theory]
        [InlineData(BuildStatus.Failed, false)]
        [InlineData(BuildStatus.Succeeded, false)]
        [InlineData(BuildStatus.InProgress, true)]
        [InlineData(BuildStatus.Stopped, false)]
        [InlineData(BuildStatus.Queued, false)]
        [InlineData(null, false)]
        public void CanGetIsInProgress(BuildStatus? status, bool expected)
        {
            Assert.Equal(expected, status.IsInProgress());
        }
    }
}
