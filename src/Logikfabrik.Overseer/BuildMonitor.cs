﻿// <copyright file="BuildMonitor.cs" company="Logikfabrik">
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
    public class BuildMonitor : IBuildMonitor, IDisposable
    {
        private readonly IBuildProviderRepository _providerRepository;
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitor" /> class.
        /// </summary>
        /// <param name="providerRepository">The provider repository.</param>
        public BuildMonitor(IBuildProviderRepository providerRepository)
        {
            Ensure.That(providerRepository).IsNotNull();

            _providerRepository = providerRepository;
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
        ///   <c>true</c> if this instance is monitoring; otherwise, <c>false</c>.
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

            _cancellationTokenSource?.Cancel();

            IsMonitoring = false;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // ReSharper disable once InvertIf
            if (disposing)
            {
                if (_cancellationTokenSource == null)
                {
                    return;
                }

                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
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
                        var buildProviders = _providerRepository.GetAll();

                        Task.WaitAll(buildProviders.Select(GetProjectsAsync).ToArray());
                    }
                    catch
                    {
                        OnError(new BuildMonitorErrorEventArgs());
                    }

                    const int delayForSeconds = 5;

                    await Task.Delay(TimeSpan.FromSeconds(delayForSeconds).Milliseconds);
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
