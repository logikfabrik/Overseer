// <copyright file="INavigable.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation
{
    /// <summary>
    /// The <see cref="INavigable" /> interface.
    /// </summary>
    public interface INavigable
    {
        /// <summary>
        /// Gets a value indicating whether this instance should be kept alive on navigation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance should be kept alive on navigation; otherwise, <c>false</c>.
        /// </value>
        bool KeepAlive { get; }
    }
}