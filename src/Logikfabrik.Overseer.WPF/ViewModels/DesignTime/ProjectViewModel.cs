// <copyright file="ProjectViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ProjectViewModel" /> class.
    /// </summary>
    public class ProjectViewModel : IProjectViewModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; } = "1234";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; } = "Overseer";

        /// <summary>
        /// Gets the build view models.
        /// </summary>
        /// <value>
        /// The build view models.
        /// </value>
        public IEnumerable<IBuildViewModel> Builds { get; } = new[] { new BuildViewModel(), new BuildViewModel(), new BuildViewModel() };

        /// <summary>
        /// Gets a value indicating whether this instance has builds.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has builds; otherwise, <c>false</c>.
        /// </value>
        public bool HasBuilds { get; } = true;

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy { get; } = false;

        /// <summary>
        /// Gets a value indicating whether this instance is errored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is errored; otherwise, <c>false</c>.
        /// </value>
        public bool IsErrored { get; } = false;

        /// <summary>
        /// Gets the digest.
        /// </summary>
        /// <value>
        /// The digest.
        /// </value>
        public IProjectDigestViewModel Digest { get; } = new ProjectDigestViewModel();

        public bool TryUpdate(string name)
        {
            throw new NotImplementedException();
        }
    }
}