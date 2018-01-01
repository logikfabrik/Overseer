// <copyright file="TrackedProjectsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;
    using Caliburn.Micro;
    using EnsureThat;
    using Gma.DataStructures.StringSearch;

    /// <summary>
    /// The <see cref="TrackedProjectsViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class TrackedProjectsViewModel : PropertyChangedBase
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly CollectionViewSource _filteredProjects;
        private readonly SuffixTrie<TrackedProjectViewModel> _trie;
        private string _filter;
        private IEnumerable<TrackedProjectViewModel> _matches;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackedProjectsViewModel" /> class.
        /// </summary>
        /// <param name="projects">The projects.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public TrackedProjectsViewModel(IEnumerable<TrackedProjectViewModel> projects)
        {
            Ensure.That(projects).IsNotNull();

            Projects = projects.ToArray();

            _filteredProjects = new CollectionViewSource
            {
                Source = Projects
            };

            _filteredProjects.Filter += (sender, e) =>
            {
                var project = (TrackedProjectViewModel)e.Item;

                e.Accepted = _matches?.Contains(project) ?? true;
            };

            FilteredProjects = _filteredProjects.View;

            _trie = new SuffixTrie<TrackedProjectViewModel>(3);

            foreach (var project in Projects)
            {
                _trie.Add(project.Name.ToLowerInvariant(), project);
            }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<TrackedProjectViewModel> Projects { get; }

        /// <summary>
        /// Gets the filtered projects.
        /// </summary>
        /// <value>
        /// The filtered projects.
        /// </value>
        public ICollectionView FilteredProjects { get; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public string Filter
        {
            get
            {
                return _filter;
            }

            set
            {
                _filter = value;
                NotifyOfPropertyChange(() => Filter);

                _matches = _trie.Retrieve(value?.ToLowerInvariant());

                FilteredProjects.Refresh();
            }
        }

        /// <summary>
        /// Track all projects.
        /// </summary>
        public void TrackAll()
        {
            ToggleTracking(true);
        }

        /// <summary>
        /// Track no projects.
        /// </summary>
        public void TrackNone()
        {
            ToggleTracking(false);
        }

        private void ToggleTracking(bool track)
        {
            var projects = FilteredProjects
                .OfType<TrackedProjectViewModel>()
                .Where(project => project.Track != track);

            foreach (var project in projects)
            {
                project.Track = track;
            }
        }
    }
}
