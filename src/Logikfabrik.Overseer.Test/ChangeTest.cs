// <copyright file="ChangeTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System;
    using Xunit;

    public class ChangeTest
    {
        [Fact]
        public void CanGetId()
        {
            var change = new Change("d131dd02c5e6eec4", DateTime.UtcNow, "John Doe", "Minor change to the build definition");

            Assert.Equal("d131dd02c5e6eec4", change.Id);
        }

        [Fact]
        public void CanGetShortId()
        {
            var change = new Change("d131dd02c5e6eec4", DateTime.UtcNow, "John Doe", "Minor change to the build definition");

            Assert.Null(change.ShortId);
        }

        [Fact]
        public void CanGetSha1Id()
        {
            var change = new Change("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", DateTime.UtcNow, "John Doe", "Minor change to the build definition");

            Assert.Equal("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", change.Id);
        }

        [Fact]
        public void CanGetSha1ShortId()
        {
            var change = new Change("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", DateTime.UtcNow, "John Doe", "Minor change to the build definition");

            Assert.Equal("2fd4e1c6", change.ShortId);
        }

        [Fact]
        public void CanGetChanged()
        {
            var changed = DateTime.UtcNow;

            var change = new Change("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", changed, "John Doe", "Minor change to the build definition");

            Assert.Equal(changed, change.Changed);
        }

        [Fact]
        public void CanGetChangedBy()
        {
            var change = new Change("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", DateTime.UtcNow, "John Doe", "Minor change to the build definition");

            Assert.Equal("John Doe", change.ChangedBy);
        }

        [Fact]
        public void CanGetComment()
        {
            var change = new Change("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", DateTime.UtcNow, "John Doe", "Minor change to the build definition");

            Assert.Equal("Minor change to the build definition", change.Comment);
        }
    }
}
