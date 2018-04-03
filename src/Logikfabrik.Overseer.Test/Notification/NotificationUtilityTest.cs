// <copyright file="NotificationUtilityTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Notification
{
    using System.Linq;
    using AutoFixture.Xunit2;
    using Overseer.Notification;
    using Shouldly;
    using Xunit;

    public class NotificationUtilityTest
    {
        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanGetPayloads(NotificationType type, int count, object payload)
        {
            var notifications = new NotificationFactory<object>().Create(type, Enumerable.Repeat(payload, count));

            NotificationUtility.GetPayloads(notifications, type, o => true).Count().ShouldBe(count);
        }
    }
}
