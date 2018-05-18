// <copyright file="ChangeTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System;
    using AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class ChangeTest
    {
        [Theory]
        [AutoData]
        public void CanGetId(string id, DateTime? changed, string changedBy, string comment)
        {
            var change = new Change(id, changed, changedBy, comment);

            change.Id.ShouldBe(id);
        }

        [Theory]
        [InlineAutoData("2fd4e1c6", null)]
        [InlineAutoData("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", "2fd4e1c6")]
        public void CanGetShortId(string id, string expected, DateTime? changed, string changedBy, string comment)
        {
            var change = new Change(id, changed, changedBy, comment);

            change.ShortId.ShouldBe(expected);
        }

        [Theory]
        [AutoData]
        public void CanGetChanged(string id, DateTime? changed, string changedBy, string comment)
        {
            var change = new Change(id, changed, changedBy, comment);

            change.Changed.ShouldBe(changed);
        }

        [Theory]
        [AutoData]
        public void CanGetChangedBy(string id, DateTime? changed, string changedBy, string comment)
        {
            var change = new Change(id, changed, changedBy, comment);

            change.ChangedBy.ShouldBe(changedBy);
        }

        [Theory]
        [AutoData]
        public void CanGetComment(string id, DateTime? changed, string changedBy, string comment)
        {
            var change = new Change(id, changed, changedBy, comment);

            change.Comment.ShouldBe(comment);
        }
    }
}
