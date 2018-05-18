// <copyright file="IConnectionPool.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using Notification;
    using Settings;

    /// <summary>
    /// The <see cref="IConnectionPool" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface IConnectionPool : IObserver<Notification<ConnectionSettings>[]>, IObservable<Notification<IConnection>[]>
    {
    }
}
