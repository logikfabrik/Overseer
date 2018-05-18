// <copyright file="EditTrackedProjectsViewModel.cs" company="Logikfabrik">
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
    using JetBrains.Annotations;

    /// <summary>
    /// The <see cref="EditTrackedProjectsViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class EditTrackedProjectsViewModel : PropertyChangedBase
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
#pragma warning disable S1450 // Private fields only used as local variables in methods should become local variables
        private readonly CollectionViewSource _filteredProjects;
#pragma warning restore S1450 // Private fields only used as local variables in methods should become local variables
        private readonly SuffixTrie<EditTrackedProjectViewModel> _trie;
        private string _filter;
        private IEnumerable<EditTrackedProjectViewModel> _matches;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTrackedProjectsViewModel" /> class.
        /// </summary>
        /// <param name="editTrackedProjectViewModels">The edit tracked project view models.</param>
        [UsedImplicitly]

        // ReSharper disable once InheritdocConsiderUsage
        public EditTrackedProjectsViewModel(IEnumerable<EditTrackedProjectViewModel> editTrackedProjectViewModels)
        {
            Ensure.That(editTrackedProjectViewModels).IsNotNull();

            Projects = editTrackedProjectViewModels.ToArray();

            _filteredProjects = new CollectionViewSource
            {
                Source = Projects
            };

            _filteredProjects.Filter += (sender, e) =>
            {
                var project = (EditTrackedProjectViewModel)e.Item;

                e.Accepted = _matches?.Contains(project) ?? true;
            };

            FilteredProjects = _filteredProjects.View;

            _trie = new SuffixTrie<EditTrackedProjectViewModel>(3);

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
        public IEnumerable<EditTrackedProjectViewModel> Projects { get; }

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

        /// <summary>
        /// Clears the filter.
        /// </summary>
        public void ClearFilter()
        {
            Filter = string.Empty;
        }

        private void ToggleTracking(bool track)
        {
            var projects = FilteredProjects
                .OfType<EditTrackedProjectViewModel>()
                .Where(project => project.Track != track);

            foreach (var project in projects)
            {
                project.Track = track;
            }
        }
    }
}
