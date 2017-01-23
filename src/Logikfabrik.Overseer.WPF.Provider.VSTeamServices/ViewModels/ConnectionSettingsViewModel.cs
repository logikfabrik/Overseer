// <copyright file="ConnectionSettingsViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.VSTeamServices.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;
    using FluentValidation;
    using Validators;
    using WPF.ViewModels;
    using WPF.ViewModels.Factories;

    /// <summary>
    /// The <see cref="ConnectionSettingsViewModel" /> class.
    /// </summary>
    public class ConnectionSettingsViewModel : WPF.ViewModels.ConnectionSettingsViewModel
    {
        private readonly IProjectToMonitorViewModelFactory _projectToMonitorFactory;
        private string _url;
        private string _token;
        private IEnumerable<ProjectToMonitorViewModel> _projectsToMonitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSettingsViewModel" /> class.
        /// </summary>
        /// <param name="projectToMonitorFactory">The project to monitor factory.</param>
        public ConnectionSettingsViewModel(IProjectToMonitorViewModelFactory projectToMonitorFactory)
        {
            Ensure.That(projectToMonitorFactory).IsNotNull();

            _projectToMonitorFactory = projectToMonitorFactory;
            Validator = new ConnectionSettingsViewModelValidator();
            _projectsToMonitor = new ProjectToMonitorViewModel[] { };
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
                NotifyOfPropertyChange(() => Url);
            }
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token
        {
            get
            {
                return _token;
            }

            set
            {
                _token = value;
                NotifyOfPropertyChange(() => Token);
            }
        }

        public IEnumerable<ProjectToMonitorViewModel> ProjectsToMonitor => _projectsToMonitor;

        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        public override IValidator Validator { get; }

        public async Task GetProjectsAsync()
        {
            if (!Validator.Validate(this).IsValid)
            {
                return;
            }

            using (var provider = new BuildProvider(new ConnectionSettings { Url = _url, Token = _token }))
            {
                var projects = await provider.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

                _projectsToMonitor = projects.Select(project => _projectToMonitorFactory.Create(project, false));

                NotifyOfPropertyChange(() => ProjectsToMonitor);
            }
        }
    }
}