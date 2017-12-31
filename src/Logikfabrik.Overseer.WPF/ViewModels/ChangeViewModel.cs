// <copyright file="ChangeViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ChangeViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ChangeViewModel : PropertyChangedBase, IChangeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeViewModel" /> class.
        /// </summary>
        /// <param name="change">The change.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ChangeViewModel(IChange change)
        {
            Ensure.That(change).IsNotNull();

            Id = change.Id;
            IdOrShortId = string.IsNullOrWhiteSpace(change.ShortId) ? change.Id : change.ShortId;
            Changed = change.Changed;
            ChangedBy = change.ChangedBy;
            Comment = change.Comment;
            ShowComment = !string.IsNullOrWhiteSpace(change.Comment);
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string IdOrShortId { get; }

        /// <inheritdoc />
        public DateTime? Changed { get; }

        /// <inheritdoc />
        public string ChangedBy { get; }

        /// <inheritdoc />
        public string Comment { get; }

        /// <inheritdoc />
        public bool ShowComment { get; }
    }
}