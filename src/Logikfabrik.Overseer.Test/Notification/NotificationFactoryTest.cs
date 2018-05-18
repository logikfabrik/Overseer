// <copyright file="NotificationFactoryTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.Notification
{
    using System.Linq;
    using AutoFixture.Xunit2;
    using Overseer.Notification;
    using Shouldly;
    using Xunit;

    public class NotificationFactoryTest
    {
        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanCreate(NotificationType type, object payload)
        {
            var notification = new NotificationFactory<object>().Create(type, payload);

            notification.ShouldNotBeNull();
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanCreateMany(NotificationType type, object[] payloads)
        {
            var notifications = new NotificationFactory<object>().Create(type, payloads);

            notifications.Length.ShouldBe(payloads.Length);
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanCreateAndGetType(NotificationType type, object payload)
        {
            var notification = new NotificationFactory<object>().Create(type, payload);

            notification.Type.ShouldBe(type);
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanCreateAndGetTypeForMany(NotificationType type, object[] payloads)
        {
            var notifications = new NotificationFactory<object>().Create(type, payloads);

            notifications.All(notification => notification.Type == type).ShouldBeTrue();
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanCreateAndGetPayload(NotificationType type, object payload)
        {
            var notification = new NotificationFactory<object>().Create(type, payload);

            notification.Payload.ShouldBe(payload);
        }

        [Theory]
        [InlineAutoData(NotificationType.Added)]
        [InlineAutoData(NotificationType.Updated)]
        [InlineAutoData(NotificationType.Removed)]
        public void CanCreateAndGetPayloadForMany(NotificationType type, object[] payloads)
        {
            var notifications = new NotificationFactory<object>().Create(type, payloads);

            notifications.All(notification => payloads.Contains(notification.Payload)).ShouldBeTrue();
        }
    }
}