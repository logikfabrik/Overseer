// <copyright file="ProjectsToMonitorViewModel.cs" company="Logikfabrik">
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
    /// The <see cref="ProjectsToMonitorViewModel" /> class.
    /// </summary>
    public class ProjectsToMonitorViewModel : PropertyChangedBase
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly CollectionViewSource _filteredProjects;
        private readonly SuffixTrie<ProjectToMonitorViewModel> _trie;
        private string _filter;
        private IEnumerable<ProjectToMonitorViewModel> _matches;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsToMonitorViewModel" /> class.
        /// </summary>
        /// <param name="projects">The projects.</param>
        public ProjectsToMonitorViewModel(IEnumerable<ProjectToMonitorViewModel> projects)
        {
            Ensure.That(projects).IsNotNull();

            Projects = projects.ToArray();

            _filteredProjects = new CollectionViewSource
            {
                Source = Projects
            };

            _filteredProjects.Filter += (sender, e) =>
            {
                var project = (ProjectToMonitorViewModel)e.Item;

                e.Accepted = _matches?.Contains(project) ?? true;
            };

            FilteredProjects = _filteredProjects.View;

            _trie = new SuffixTrie<ProjectToMonitorViewModel>(3);

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
        public IEnumerable<ProjectToMonitorViewModel> Projects { get; }

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
        /// Monitor all projects.
        /// </summary>
        public void MonitorAll()
        {
            ToggleMonitoring(true);
        }

        /// <summary>
        /// Monitor no projects.
        /// </summary>
        public void MonitorNone()
        {
            ToggleMonitoring(false);
        }

        private void ToggleMonitoring(bool monitor)
        {
            var projects = FilteredProjects
                .OfType<ProjectToMonitorViewModel>()
                .Where(project => project.Monitor != monitor);

            foreach (var project in projects)
            {
                project.Monitor = monitor;
            }
        }
    }
}
