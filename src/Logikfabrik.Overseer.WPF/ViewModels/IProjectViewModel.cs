// <copyright file="IProjectViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IProjectViewModel" /> interface.
    /// </summary>
    public interface IProjectViewModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <value>
        /// The builds.
        /// </value>
        IEnumerable<IBuildViewModel> Builds { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has builds.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has builds; otherwise, <c>false</c>.
        /// </value>
        bool HasBuilds { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        bool IsBusy { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        bool IsErrored { get; }

        /// <summary>
        /// Gets the digest.
        /// </summary>
        /// <value>
        /// The digest.
        /// </value>
        IProjectDigestViewModel Digest { get; }

        bool TryUpdate(string name);
    }
}