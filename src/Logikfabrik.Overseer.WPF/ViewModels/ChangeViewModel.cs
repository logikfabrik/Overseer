// <copyright file="ChangeViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class ChangeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeViewModel" /> class.
        /// </summary>
        /// <param name="change">The change.</param>
        public ChangeViewModel(IChange change)
        {
            Ensure.That(change).IsNotNull();

            Changed = change.Changed;
            ChangedBy = change.ChangedBy;
            Comment = change.Comment;
        }

        /// <summary>
        /// Gets the changed date.
        /// </summary>
        /// <value>
        /// The changed date.
        /// </value>
        public DateTime? Changed { get; }

        /// <summary>
        /// Gets the name of whoever made the last change.
        /// </summary>
        /// <value>
        /// The name of whoever made the last change.
        /// </value>
        public string ChangedBy { get; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; }
    }
}
