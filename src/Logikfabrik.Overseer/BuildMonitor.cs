// <copyright file="BuildMonitor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildMonitor" /> class.
    /// </summary>
    public class BuildMonitor : IBuildMonitor
    {
        private readonly IBuildProviderRepository _buildProviderRepository;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitor" /> class.
        /// </summary>
        /// <param name="buildProviderRepository">The build provider repository.</param>
        public BuildMonitor(IBuildProviderRepository buildProviderRepository)
        {
            Ensure.That(buildProviderRepository).IsNotNull();

            _buildProviderRepository = buildProviderRepository;
        }

        /// <summary>
        /// Occurs when progress changes.
        /// </summary>
        public event EventHandler<BuildMonitorProgressEventArgs> ProgressChanged;

        /// <summary>
        /// Occurs if there is an error.
        /// </summary>
        public event EventHandler<BuildMonitorErrorEventArgs> Error;

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

            Monitor();

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

            _cancellationTokenSource.Cancel();

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

        /// <summary>
        /// Raises the <see cref="Error" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildMonitorErrorEventArgs" /> instance containing the event data.</param>
        protected virtual void OnError(BuildMonitorErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }

        private void Monitor()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            // ReSharper disable once FunctionNeverReturns
            Task.Run(
                async () =>
            {
                while (true)
                {
                    try
                    {
                        var buildProviders = _buildProviderRepository.GetBuildProviders();

                        Task.WaitAll(buildProviders.Select(GetProjectsAsync).ToArray());
                    }
                    catch
                    {
                        OnError(new BuildMonitorErrorEventArgs());
                    }

                    const int delayInSeconds = 5;
                    const int delayInMilliseconds = delayInSeconds * 1000;

                    await Task.Delay(delayInMilliseconds);
                }
            },
            _cancellationTokenSource.Token);
        }

        private async Task GetProjectsAsync(IBuildProvider buildProvider)
        {
            try
            {
                var projects = await buildProvider.GetProjectsAsync().ConfigureAwait(false);

                Task.WaitAll(projects.Select(async project =>
                {
                    try
                    {
                        var builds = await buildProvider.GetBuildsAsync(project.Id).ConfigureAwait(false);

                        OnProgressChanged(new BuildMonitorProgressEventArgs(buildProvider, project, builds));
                    }
                    catch (Exception)
                    {
                        OnError(new BuildMonitorErrorEventArgs(buildProvider, project));
                    }
                }).ToArray());
            }
            catch
            {
                OnError(new BuildMonitorErrorEventArgs(buildProvider));
            }
        }
    }
}
