// <copyright file="ChangeViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;

    /// <summary>
    /// The <see cref="ChangeViewModel" /> class.
    /// </summary>
    public class ChangeViewModel : IChangeViewModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; } = "1234";

        /// <summary>
        /// Gets the changed date.
        /// </summary>
        /// <value>
        /// The changed date.
        /// </value>
        public DateTime? Changed { get; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the name of whoever made the change.
        /// </summary>
        /// <value>
        /// The name of whoever made the change.
        /// </value>
        public string ChangedBy { get; } = "John Doe";

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; } = "Minor change to the build definition";

        /// <summary>
        /// Gets a value indicating whether to show the comment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the comment should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowComment { get; } = true;
    }
}
