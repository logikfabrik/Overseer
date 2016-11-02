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

            Id = change.Id;
            Comment = change.Comment;
            Message = GetMessage(change);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        private static string GetMessage(IChange change)
        {
            return $"{(!string.IsNullOrWhiteSpace(change.ChangedBy) ? $"by {change.ChangedBy}" : string.Empty)} {(change.Changed.HasValue ? $"{(DateTime.UtcNow - change.Changed).Value.Humanize()} ago" : string.Empty)}";
        }
    }
}
