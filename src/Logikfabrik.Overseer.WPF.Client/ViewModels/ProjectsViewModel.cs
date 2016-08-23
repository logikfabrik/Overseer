// <copyright file="ProjectsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System.Collections.ObjectModel;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ProjectsViewModel" /> class.
    /// </summary>
    public class ProjectsViewModel : PropertyChangedBase
    {
        private ObservableCollection<ProjectViewModel> _projects;

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                return _projects;
            }

            set
            {
                _projects = value;
                NotifyOfPropertyChange(() => Projects);
            }
        }
    }
}
