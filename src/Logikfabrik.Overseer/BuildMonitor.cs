// <copyright file="BuildMonitor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildMonitor" /> class.
    /// </summary>
    public class BuildMonitor
    {
        private readonly BackgroundWorker _backgroundWorker;
        private readonly IBuildProviderRepository _buildProviderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitor" /> class.
        /// </summary>
        /// <param name="buildProviderRepository">The build provider repository.</param>
        public BuildMonitor(IBuildProviderRepository buildProviderRepository)
        {
            Ensure.That(buildProviderRepository).IsNotNull();

            if (buildProviderRepository == null)
            {
                throw new ArgumentNullException(nameof(buildProviderRepository));
            }

            _buildProviderRepository = buildProviderRepository;

            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            _backgroundWorker.DoWork += DoWork;
            _backgroundWorker.ProgressChanged += ProgressChanged;
        }

        /// <summary>
        /// Occurs when a build status changes.
        /// </summary>
        public event EventHandler<BuildStatusChangedEventArgs> BuildStatusChanged;

        /// <summary>
        /// Raises the <see cref="BuildStatusChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildStatusChangedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnBuildStatusChanged(BuildStatusChangedEventArgs e)
        {
            BuildStatusChanged?.Invoke(this, e);
        }

        private static int GetPercentProgress(int totalProviders, int currentProvider, int currentProviderTotalProjects, int currentProviderProject)
        {
            var totalProviderProgress = (double)currentProvider / totalProviders;

            var currentProviderProgress = ((double)currentProviderProject / currentProviderTotalProjects) * ((double)1 / totalProviders);

            return (int)Math.Floor(totalProviderProgress + currentProviderProgress) * 100;
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var state = (BuildMonitorState)e.UserState;

            var args = new BuildStatusChangedEventArgs(e.ProgressPercentage, state.Provider, state.Project, state.Builds);

            OnBuildStatusChanged(args);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var providers = _buildProviderRepository.GetProviders().ToArray();

            for (var i = 0; i < providers.Length; i++)
            {
                if (_backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var projects = providers[i].GetProjects().ToArray();

                for (var j = 0; j < projects.Length; j++)
                {
                    if (_backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    var builds = providers[i].GetBuilds(projects[j].Id);

                    var percentProgress = GetPercentProgress(providers.Length, i, projects.Length, j);

                    _backgroundWorker.ReportProgress(percentProgress, new BuildMonitorState(providers[i], projects[j], builds));
                }
            }
        }
    }
}
