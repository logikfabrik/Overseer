// <copyright file="NotificationTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test
{
    using System.Linq;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class NotificationTest
    {
        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanCreate(NotificationType type, object payload)
        {
            var notification = Notification<object>.Create(type, payload);

            notification.ShouldNotBeNull();
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanCreateMany(NotificationType type, object[] payloads)
        {
            var notifications = Notification<object>.Create(type, payloads);

            notifications.Length.ShouldBe(payloads.Length);
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanGetType(NotificationType type, object payload)
        {
            var notification = Notification<object>.Create(type, payload);

            notification.Type.ShouldBe(type);
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanGetTypeForMany(NotificationType type, object[] payloads)
        {
            var notifications = Notification<object>.Create(type, payloads);

            notifications.All(notification => notification.Type == type).ShouldBeTrue();
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanGetPayload(NotificationType type, object payload)
        {
            var notification = Notification<object>.Create(type, payload);

            notification.Payload.ShouldBe(payload);
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanGetPayloadForMany(NotificationType type, object[] payloads)
        {
            var notifications = Notification<object>.Create(type, payloads);

            notifications.All(notification => payloads.Contains(notification.Payload)).ShouldBeTrue();
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanGetPayloads(NotificationType type, int count, object payload)
        {
            var notifications = Notification<object>.Create(type, Enumerable.Repeat(payload, count));

            Notification<object>.GetPayloads(notifications, type, o => true).Count().ShouldBe(count);
        }
    }
}
