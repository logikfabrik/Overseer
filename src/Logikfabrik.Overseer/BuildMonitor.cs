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
    public class BuildMonitor : IBuildMonitor, IDisposable, IObserver<Connection[]>
    {
        private readonly IDisposable _subscription;
        private readonly IDictionary<Guid, IProject> _projects;
        private readonly IDictionary<Tuple<Guid, string>, IEnumerable<IBuild>> _projectBuilds;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitor" /> class.
        /// </summary>
        /// <param name="connectionPool">The connection pool.</param>
        public BuildMonitor(IConnectionPool connectionPool)
        {
            Ensure.That(connectionPool).IsNotNull();

            _cancellationTokenSource = new CancellationTokenSource();
            _projects = new Dictionary<Guid, IProject>();
            _projectBuilds = new Dictionary<Tuple<Guid, string>, IEnumerable<IBuild>>();
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
        public void OnNext(Connection[] value)
        {
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            // ReSharper disable once FunctionNeverReturns
            Task.Run(
                async () =>
                {
                    while (true)
                    {
                        if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
                        {
                            return;
                        }

                        Task.WaitAll(value.Select(GetProjectsAsync).ToArray(), _cancellationTokenSource.Token);

                        const int delayForSeconds = 15;

                        await Task.Delay(TimeSpan.FromSeconds(delayForSeconds).Milliseconds).ConfigureAwait(false);
                    }
                },
                _cancellationTokenSource.Token);
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

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // ReSharper disable once InvertIf
            if (disposing)
            {
                // ReSharper disable once UseNullPropagation
                if (_subscription != null)
                {
                    _subscription.Dispose();
                }

                // ReSharper disable once UseNullPropagation
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Dispose();
                }
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

        private async Task GetProjectsAsync(Connection connection)
        {
            try
            {
                if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }

                var projects = (await connection.GetProjectsAsync().ConfigureAwait(false)).ToArray();

                OnConnectionProgressChanged(new BuildMonitorConnectionProgressEventArgs(connection.Settings.Id, projects));

                Task.WaitAll(
                    projects.Select(async project =>
                    {
                        if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
                        {
                            return;
                        }

                        try
                        {
                            AddOrUpdateProject(connection, project);

                            var builds = (await connection.GetBuildsAsync(project).ConfigureAwait(false)).ToArray();

                            AddOrUpdateProjectBuilds(connection, project, builds);

                            OnProjectProgressChanged(new BuildMonitorProjectProgressEventArgs(connection.Settings.Id, project, builds));
                        }
                        catch (OperationCanceledException)
                        {
                            // Do nothing.
                        }
                        catch
                        {
                            OnProjectError(new BuildMonitorProjectErrorEventArgs(connection.Settings.Id, project));
                        }
                    }).ToArray(), _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // Do nothing.
            }
            catch
            {
                OnConnectionError(new BuildMonitorConnectionErrorEventArgs(connection.Settings.Id));
            }
        }

        private void AddOrUpdateProject(Connection connection, IProject project)
        {
            var key = connection.Settings.Id;

            if (_projects.ContainsKey(key))
            {
                _projects[key] = project;
            }
            else
            {
                _projects.Add(key, project);
            }
        }

        private void AddOrUpdateProjectBuilds(Connection connection, IProject project, IEnumerable<IBuild> builds)
        {
            var key = new Tuple<Guid, string>(connection.Settings.Id, project.Id);

            if (_projectBuilds.ContainsKey(key))
            {
                _projectBuilds[key] = builds;
            }
            else
            {
                _projectBuilds.Add(key, builds);
            }
        }
    }
}
