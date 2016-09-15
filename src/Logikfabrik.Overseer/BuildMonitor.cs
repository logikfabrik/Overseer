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
    public class BuildMonitor : IBuildMonitor
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

            _buildProviderRepository = buildProviderRepository;

            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            _backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
            _backgroundWorker.ProgressChanged += OnBackgroundWorkerProgressChanged;
            _backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
        }

        /// <summary>
        /// Occurs when progress changes.
        /// </summary>
        public event EventHandler<BuildMonitorProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Gets a value indicating whether this instance is monitoring.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is monitoring; otherwise, <c>false</c>.
        /// </value>
        public bool IsMonitoring { get; private set; }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        public void StartMonitoring()
        {
            if (IsMonitoring)
            {
                return;
            }

            _backgroundWorker.RunWorkerAsync();

            IsMonitoring = true;
        }

        /// <summary>
        /// Stops the monitoring.
        /// </summary>
        public void StopMonitoring()
        {
            if (!IsMonitoring)
            {
                return;
            }

            _backgroundWorker.CancelAsync();

            IsMonitoring = false;
        }

        /// <summary>
        /// Raises the <see cref="ProgressChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildMonitorProgressEventArgs" /> instance containing the event data.</param>
        protected virtual void OnProgressChanged(BuildMonitorProgressEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        private static int GetPercentProgress(int totalProviders, int currentProvider, int currentProviderTotalProjects, int currentProviderProject)
        {
            var totalProviderProgress = (double)currentProvider / totalProviders;

            var currentProviderProgress = (double)currentProviderProject / currentProviderTotalProjects * ((double)1 / totalProviders);

            return (int)Math.Floor((totalProviderProgress + currentProviderProgress) * 100);
        }

        private void OnBackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var state = (BuildMonitorState)e.UserState;

            var args = new BuildMonitorProgressEventArgs(e.ProgressPercentage, state.Provider, state.Project, state.Builds);

            OnProgressChanged(args);
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
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

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                throw e.Error;
            }

            if (!IsMonitoring)
            {
                return;
            }

            _backgroundWorker.RunWorkerAsync();
        }
    }
}
