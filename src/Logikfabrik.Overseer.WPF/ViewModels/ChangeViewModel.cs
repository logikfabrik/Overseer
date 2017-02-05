// <copyright file="ChangeViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;
    using Humanizer;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class ChangeViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeViewModel" /> class.
        /// </summary>
        /// <param name="change">The change.</param>
        public ChangeViewModel(IChange change)
        {
            Ensure.That(change).IsNotNull();

            Id = change.Id.Truncate(10, "...");
            Changed = change.Changed;
            ChangedBy = change.ChangedBy;
            Comment = change.Comment;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the changed date.
        /// </summary>
        /// <value>
        /// The changed date.
        /// </value>
        public DateTime? Changed { get; }

        /// <summary>
        /// Gets the name of whoever made the change.
        /// </summary>
        /// <value>
        /// The name of whoever made the change.
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
