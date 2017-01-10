// <copyright file="BuildMonitor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EnsureThat;

    /// <summary>
    /// The <see cref="BuildMonitor" /> class.
    /// </summary>
    public class BuildMonitor : IBuildMonitor, IDisposable, IObserver<IConnection[]>
    {
        private readonly IDisposable _subscription;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isDisposed;
        private Task _poll;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitor" /> class.
        /// </summary>
        /// <param name="connectionPool">The connection pool.</param>
        public BuildMonitor(IConnectionPool connectionPool)
        {
            Ensure.That(connectionPool).IsNotNull();

            _subscription = connectionPool.Subscribe(this);
        }

        /// <summary>
        /// Occurs if there is an connection error.
        /// </summary>
        public event EventHandler<BuildMonitorConnectionErrorEventArgs> ConnectionError;

        /// <summary>
        /// Occurs when connection progress changes.
        /// </summary>
        public event EventHandler<BuildMonitorConnectionProgressEventArgs> ConnectionProgressChanged;

        /// <summary>
        /// Occurs if there is an project error.
        /// </summary>
        public event EventHandler<BuildMonitorProjectErrorEventArgs> ProjectError;

        /// <summary>
        /// Occurs when project progress changes.
        /// </summary>
        public event EventHandler<BuildMonitorProjectProgressEventArgs> ProjectProgressChanged;

        /// <summary>
        /// Provides the observer with new data.
        /// </summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(IConnection[] value)
        {
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            if (_poll != null)
            {
                _cancellationTokenSource.Cancel();

                try
                {
                    _poll.Wait();
                }
                catch (AggregateException aex)
                {
                    aex.Flatten().Handle(ex => ex is OperationCanceledException);

                    throw;
                }
            }

            _cancellationTokenSource = new CancellationTokenSource();

            _poll = Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    await GetProjectsAndBuildsAsync(value, _cancellationTokenSource.Token).ConfigureAwait(false);
                }
            });
        }

        /// <summary>
        /// Notifies the observer that the provider has experienced an error condition.
        /// </summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
            // Do nothing, even if disposed (pattern practice).
        }

        /// <summary>
        /// Notifies the observer that the provider has finished sending push-based notifications.
        /// </summary>
        public void OnCompleted()
        {
            // Do nothing, even if disposed (pattern practice).
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal async Task GetProjectsAndBuildsAsync(IEnumerable<IConnection> connections, CancellationToken cancellationToken)
        {
            var tasks = connections.Select(connection => GetProjectsAsync(connection, cancellationToken)).Concat(new[] { Task.Delay(TimeSpan.FromSeconds(15), cancellationToken) });

            await Task.WhenAll(tasks);
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
                if (_poll != null)
                {
                    _cancellationTokenSource.Cancel();

                    try
                    {
                        _poll.Wait();
                    }
                    catch (Exception)
                    {
                        // Do not throw exceptions while disposing.
                    }
                }

                _subscription.Dispose();

                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Raises the <see cref="ConnectionError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildMonitorConnectionErrorEventArgs" /> instance containing the event data.</param>
        protected virtual void OnConnectionError(BuildMonitorConnectionErrorEventArgs e)
        {
            ConnectionError?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ConnectionProgressChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildMonitorConnectionProgressEventArgs" /> instance containing the event data.</param>
        protected virtual void OnConnectionProgressChanged(BuildMonitorConnectionProgressEventArgs e)
        {
            ConnectionProgressChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ProjectError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildMonitorProjectErrorEventArgs" /> instance containing the event data.</param>
        protected virtual void OnProjectError(BuildMonitorProjectErrorEventArgs e)
        {
            ProjectError?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ProjectProgressChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildMonitorProjectProgressEventArgs" /> instance containing the event data.</param>
        protected virtual void OnProjectProgressChanged(BuildMonitorProjectProgressEventArgs e)
        {
            ProjectProgressChanged?.Invoke(this, e);
        }

        private async Task GetProjectsAsync(IConnection connection, CancellationToken cancellationToken)
        {
            try
            {
                var projects = (await connection.GetProjectsAsync(cancellationToken)).ToArray();

                OnConnectionProgressChanged(new BuildMonitorConnectionProgressEventArgs(connection.Settings.Id, projects));

                var tasks = projects.Select(project => GetBuildsAsync(connection, project, cancellationToken)).ToArray();

                await Task.WhenAll(tasks);
            }
            catch (Exception)
            {
                OnConnectionError(new BuildMonitorConnectionErrorEventArgs(connection.Settings.Id));

                throw;
            }
        }

        private async Task GetBuildsAsync(IConnection connection, IProject project, CancellationToken cancellationToken)
        {
            try
            {
                var builds = await connection.GetBuildsAsync(project, cancellationToken).ConfigureAwait(false);

                OnProjectProgressChanged(new BuildMonitorProjectProgressEventArgs(connection.Settings.Id, project, builds));
            }
            catch (Exception)
            {
                OnProjectError(new BuildMonitorProjectErrorEventArgs(connection.Settings.Id, project));

                throw;
            }
        }
    }
}