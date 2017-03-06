﻿// <copyright file="ProjectsToMonitorViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ProjectsToMonitorViewModel" /> class.
    /// </summary>
    public class ProjectsToMonitorViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsToMonitorViewModel" /> class.
        /// </summary>
        /// <param name="projects">The projects.</param>
        public ProjectsToMonitorViewModel(IEnumerable<ProjectToMonitorViewModel> projects)
        {
            Ensure.That(projects).IsNotNull();

            Projects = new BindableCollection<ProjectToMonitorViewModel>(projects);
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public BindableCollection<ProjectToMonitorViewModel> Projects { get; }

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
            var projects = Projects.Where(project => project.Monitor != monitor).ToArray();

            foreach (var project in projects)
            {
                project.Monitor = monitor;
            }
        }
    }
}
