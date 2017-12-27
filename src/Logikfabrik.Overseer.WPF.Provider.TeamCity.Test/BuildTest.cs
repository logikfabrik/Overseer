// <copyright file="BuildTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class BuildTest
    {
        [Theory]
        [AutoData]
        public void CanGetId(string id)
        {
            var build = new Build(new Api.Models.Build
            {
                Id = id
            });

            build.Id.ShouldBe(id);
        }

        [Fact]
        public void CanGetVersion()
        {
            var build = new Build(new Api.Models.Build());

            build.Version.ShouldBeNull();
        }

        [Theory]
        [AutoData]
        public void CanGetNumber(string number)
        {
            var build = new Build(new Api.Models.Build
            {
                Number = number
            });

            build.Number.ShouldBe(number);
        }

        [Theory]
        [AutoData]
        public void CanGetBranch(string branchName)
        {
            var build = new Build(new Api.Models.Build
            {
                BranchName = branchName
            });

            build.Branch.ShouldBe(branchName);
        }

        [Theory]
        [ClassData(typeof(CanGetTimeClassData))]
        public void CanGetStartTime(DateTime? startDate)
        {
            var build = new Build(new Api.Models.Build
            {
                StartDate = startDate
            });

            build.StartTime.ShouldBe(startDate?.ToUniversalTime());
        }

        [Theory]
        [ClassData(typeof(CanGetTimeClassData))]
        public void CanGetEndTime(DateTime? finishDate)
        {
            var build = new Build(new Api.Models.Build
            {
                FinishDate = finishDate
            });

            build.EndTime.ShouldBe(finishDate?.ToUniversalTime());
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(Api.Models.BuildState.Queued, null, BuildStatus.Queued)]
        [InlineData(Api.Models.BuildState.Running, null, BuildStatus.InProgress)]
        [InlineData(Api.Models.BuildState.Finished, null, null)]
        [InlineData(Api.Models.BuildState.Finished, Api.Models.BuildStatus.Success, BuildStatus.Succeeded)]
        [InlineData(Api.Models.BuildState.Finished, Api.Models.BuildStatus.Failure, BuildStatus.Failed)]
        [InlineData(Api.Models.BuildState.Finished, Api.Models.BuildStatus.Error, BuildStatus.Failed)]
        public void CanGetStatus(Api.Models.BuildState? state, Api.Models.BuildStatus? status, BuildStatus? expected)
        {
            var build = new Build(new Api.Models.Build
            {
                State = state,
                Status = status
            });

            build.Status.ShouldBe(expected);
        }

        [Theory]
        [AutoData]
        public void CanGetRequestedBy(string username)
        {
            var build = new Build(new Api.Models.Build
            {
                Triggered = new Api.Models.Trigger
                {
                    User = new Api.Models.User { Username = username }
                }
            });

            build.RequestedBy.ShouldBe(username);
        }

        [Theory]
        [AutoData]
        public void CanGetWebUrl(Uri webUrl)
        {
            var build = new Build(new Api.Models.Build
            {
                WebUrl = webUrl
            });

            build.WebUrl.ShouldBe(webUrl);
        }

        [Fact]
        public void CanGetChanges()
        {
            var build = new Build(new Api.Models.Build
            {
                LastChanges = new Api.Models.Changes { Change = new[] { new Api.Models.Change() } }
            });

            build.Changes.Count().ShouldBe(1);
        }

        private class CanGetTimeClassData : IEnumerable<object[]>
        {
            private readonly IEnumerable<object[]> _data = new[]
            {
                new object[] { null },
                new object[] { DateTime.UtcNow },
                new object[] { DateTime.UtcNow.ToUniversalTime() }
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}