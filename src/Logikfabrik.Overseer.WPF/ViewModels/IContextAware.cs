// <copyright file="IContextAware.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    /// <summary>
    /// The <see cref="IContextAware" /> interface.
    /// </summary>
    public interface IContextAware
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        string Context { get; set; }
    }
}