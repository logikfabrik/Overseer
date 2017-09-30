// <copyright file="BuildStatusExtensionsTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Extensions
{
    using Overseer.Extensions;
    using Shouldly;
    using Xunit;

    public class BuildStatusExtensionsTest
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData(BuildStatus.Failed, true)]
        [InlineData(BuildStatus.Succeeded, true)]
        [InlineData(BuildStatus.InProgress, false)]
        [InlineData(BuildStatus.Stopped, true)]
        [InlineData(BuildStatus.Queued, false)]
        public void CanGetIsFinished(BuildStatus? status, bool expected)
        {
            status.IsFinished().ShouldBe(expected);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(BuildStatus.Failed, false)]
        [InlineData(BuildStatus.Succeeded, false)]
        [InlineData(BuildStatus.InProgress, true)]
        [InlineData(BuildStatus.Stopped, false)]
        [InlineData(BuildStatus.Queued, false)]
        public void CanGetIsInProgress(BuildStatus? status, bool expected)
        {
            status.IsInProgress().ShouldBe(expected);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(BuildStatus.Failed, false)]
        [InlineData(BuildStatus.Succeeded, false)]
        [InlineData(BuildStatus.InProgress, false)]
        [InlineData(BuildStatus.Stopped, false)]
        [InlineData(BuildStatus.Queued, true)]
        public void CanGetIsQueued(BuildStatus? status, bool expected)
        {
            status.IsQueued().ShouldBe(expected);
        }
    }
}
