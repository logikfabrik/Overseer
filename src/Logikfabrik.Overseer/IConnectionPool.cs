// <copyright file="IConnectionPool.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using Settings;

    /// <summary>
    /// The <see cref="IConnectionPool" /> class.
    /// </summary>
    public interface IConnectionPool : IObserver<ConnectionSettings[]>, IObservable<Connection[]>, IDisposable
    {
    }
}
