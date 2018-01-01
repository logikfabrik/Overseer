// <copyright file="TrackedProjectViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="TrackedProjectViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class TrackedProjectViewModel : PropertyChangedBase
    {
        private bool _track;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackedProjectViewModel" /> class.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="projectName">The project name.</param>
        /// <param name="track">Whether this project should be tracked.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public TrackedProjectViewModel(string projectId, string projectName, bool track)
        {
            Ensure.That(projectId).IsNotNullOrWhiteSpace();
            Ensure.That(projectName).IsNotNullOrWhiteSpace();

            Id = projectId;
            Name = projectName;
            _track = track;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this project should be tracked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this project should be tracked; otherwise, <c>false</c>.
        /// </value>
        public bool Track
        {
            get
            {
                return _track;
            }

            set
            {
                _track = value;
                NotifyOfPropertyChange(() => Track);
            }
        }
    }
}
