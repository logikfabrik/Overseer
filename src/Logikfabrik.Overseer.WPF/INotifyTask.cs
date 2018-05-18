// <copyright file="INotifyTask.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Threading.Tasks;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="INotifyTask" /> interface.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public interface INotifyTask : INotifyPropertyChangedEx
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        TaskStatus? Status { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        Exception Exception { get; }
    }
}
