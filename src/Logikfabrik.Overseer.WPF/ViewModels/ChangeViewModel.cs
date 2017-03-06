// <copyright file="ChangeViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;

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

            Id = change.Id;
            ShowId = !string.IsNullOrWhiteSpace(change.Id);
            Changed = change.Changed;
            ShowChanged = change.Changed.HasValue;
            ChangedBy = change.ChangedBy;
            ShowChangedBy = !string.IsNullOrWhiteSpace(change.ChangedBy);
            Comment = change.Comment;
            ShowComment = !string.IsNullOrWhiteSpace(change.Comment);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets a value indicating whether to show the identifier.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the identifier should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowId { get; }

        /// <summary>
        /// Gets the changed date.
        /// </summary>
        /// <value>
        /// The changed date.
        /// </value>
        public DateTime? Changed { get; }

        /// <summary>
        /// Gets a value indicating whether to show the changed date.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the changed date should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowChanged { get; }

        /// <summary>
        /// Gets the name of whoever made the change.
        /// </summary>
        /// <value>
        /// The name of whoever made the change.
        /// </value>
        public string ChangedBy { get; }

        /// <summary>
        /// Gets a value indicating whether to show the name of whoever made the changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the name of whoever made the change should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowChangedBy { get; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; }

        /// <summary>
        /// Gets a value indicating whether to show the comment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the comment should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowComment { get; }
    }
}