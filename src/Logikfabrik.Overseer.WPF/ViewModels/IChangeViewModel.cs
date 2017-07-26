// <copyright file="IChangeViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;

    /// <summary>
    /// The <see cref="IChangeViewModel" /> interface.
    /// </summary>
    public interface IChangeViewModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the changed date.
        /// </summary>
        /// <value>
        /// The changed date.
        /// </value>
        DateTime? Changed { get; }

        /// <summary>
        /// Gets the name of whoever made the change.
        /// </summary>
        /// <value>
        /// The name of whoever made the change.
        /// </value>
        string ChangedBy { get; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        string Comment { get; }

        /// <summary>
        /// Gets a value indicating whether to show the comment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the comment should be shown; otherwise, <c>false</c>.
        /// </value>
        bool ShowComment { get; }
    }
}