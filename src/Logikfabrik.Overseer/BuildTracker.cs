// <copyright file="BuildTracker.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using EnsureThat;
    using JetBrains.Annotations;
    using Logging;
    using Notification;

    /// <summary>
    /// The <see cref="BuildTracker" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class BuildTracker : IBuildTracker, IDisposable
    {
        private readonly IBuildTrackerSettingsFactory _buildTrackerSettingsFactory;
        private readonly ILogService _logService;
        private IDictionary<Guid, IConnection> _connections;
        private IDisposable _subscription;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTracker" /> class.
        /// </summary>
        /// <param name="connectionPool">The connection pool.</param>
        /// <param name="buildTrackerSettingsFactory">The build tracker settings factory.</param>
        /// <param name="logService">The log service.</param>
        [UsedImplicitly]
        public BuildTracker(IConnectionPool connectionPool, IBuildTrackerSettingsFactory buildTrackerSettingsFactory, ILogService logService)
        {
            Ensure.That(connectionPool).IsNotNull();
            Ensure.That(buildTrackerSettingsFactory).IsNotNull();
            Ensure.That(logService).IsNotNull();

            _buildTrackerSettingsFactory = buildTrackerSettingsFactory;
            _logService = logService;
            _connections = new Dictionary<Guid, IConnection>();
            _subscription = connectionPool.Subscribe(this);
        }

        /// <inheritdoc />
        public event EventHandler<BuildTrackerConnectionErrorEventArgs> ConnectionError;

        /// <inheritdoc />
        public event EventHandler<BuildTrackerConnectionProgressEventArgs> ConnectionProgressChanged;

        /// <inheritdoc />
        public event EventHandler<BuildTrackerProjectErrorEventArgs> ProjectError;

        /// <inheritdoc />
        public event EventHandler<BuildTrackerProjectProgressEventArgs> ProjectProgressChanged;

        /// <inheritdoc />
        public void OnNext(Notification<IConnection>[] value)
        {
            if (_isDisposed)
            {
                // Do nothing if disposed (pattern practice).
                return;
            }

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            var cancellationToken = _cancellationTokenSource.Token;

            // ReSharper disable once FunctionNeverReturns
            Task.Factory.StartNew(
                async () =>
                {
                    while (true)
                    {
                        try
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            UpdateConnections(_connections, value);

                            await GetProjectsAndBuildsAsync(_connections.Values, cancellationToken).ConfigureAwait(false);

                            await Task.Delay(TimeSpan.FromSeconds(_buildTrackerSettingsFactory.Create().Interval), cancellationToken).ConfigureAwait(false);
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            _logService.Log(GetType(), new LogEntry(LogEntryType.Error, "An error occurred while polling.", ex));
                        }
                    }
                }, cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
        }

        /// <inheritdoc />
        public void OnError(Exception error)
        {
            // Do nothing, even if disposed (pattern practice).
        }

        /// <inheritdoc />
        public void OnCompleted()
        {
            // Do nothing, even if disposed (pattern practice).
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal static void UpdateConnections(IDictionary<Guid, IConnection> connections, Notification<IConnection>[] notifications)
        {
            foreach (var connection in NotificationUtility.GetPayloads(notifications, NotificationType.Removed, c => connections.ContainsKey(c.Settings.Id)))
            {
                connections.Remove(connection.Settings.Id);
            }

            foreach (var connection in NotificationUtility.GetPayloads(notifications, NotificationType.Added, connection => !connections.ContainsKey(connection.Settings.Id)))
            {
                connections.Add(connection.Settings.Id, connection);
            }
        }

        /// <summary>
        /// Gets the projects for the specified connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        internal async Task<IEnumerable<IProject>> GetProjectsAsync(IConnection connection, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var projects =
                    (await connection.GetProjectsAsync(cancellationToken).ConfigureAwait(false)).Where(
                        project => connection.Settings.TrackedProjects.Contains(project.Id)).ToArray();

                OnConnectionProgressChanged(new BuildTrackerConnectionProgressEventArgs(connection.Settings.Id, projects));

                return projects;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                OnConnectionError(new BuildTrackerConnectionErrorEventArgs(connection.Settings.Id));

                _logService.Log(GetType(), new LogEntry(LogEntryType.Error, "An unexpected error occurred while polling projects.", ex));

                return new IProject[] { };
            }
        }

        /// <summary>
        /// Gets the builds for the specified connection and project.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="project">The project.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        internal async Task GetBuildsAsync(IConnection connection, IProject project, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var builds = await connection.GetBuildsAsync(project, cancellationToken).ConfigureAwait(false);

                OnProjectProgressChanged(new BuildTrackerProjectProgressEventArgs(connection.Settings.Id, project, builds));
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                OnProjectError(new BuildTrackerProjectErrorEventArgs(connection.Settings.Id, project));

                _logService.Log(GetType(), new LogEntry(LogEntryType.Error, "An unexpected error occurred while polling builds.", ex));
            }
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }

                if (_connections != null)
                {
                    _connections.Clear();
                    _connections = null;
                }

                if (_subscription != null)
                {
                    _subscription.Dispose();
                    _subscription = null;
                }
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Raises the <see cref="ConnectionError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildTrackerConnectionErrorEventArgs" /> instance containing the event data.</param>
        protected virtual void OnConnectionError(BuildTrackerConnectionErrorEventArgs e)
        {
            ConnectionError?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ConnectionProgressChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildTrackerConnectionProgressEventArgs" /> instance containing the event data.</param>
        protected virtual void OnConnectionProgressChanged(BuildTrackerConnectionProgressEventArgs e)
        {
            ConnectionProgressChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ProjectError" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildTrackerProjectErrorEventArgs" /> instance containing the event data.</param>
        protected virtual void OnProjectError(BuildTrackerProjectErrorEventArgs e)
        {
            ProjectError?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ProjectProgressChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="BuildTrackerProjectProgressEventArgs" /> instance containing the event data.</param>
        protected virtual void OnProjectProgressChanged(BuildTrackerProjectProgressEventArgs e)
        {
            ProjectProgressChanged?.Invoke(this, e);
        }

        private async Task GetProjectsAndBuildsAsync(IEnumerable<IConnection> connections, CancellationToken cancellationToken)
        {
            var projectBufferBlock = new BufferBlock<IConnection>(new DataflowBlockOptions
            {
                BoundedCapacity = 8,
                CancellationToken = cancellationToken
            });

            var buildsBufferBlock = new BufferBlock<Tuple<IConnection, IProject>>(new DataflowBlockOptions
            {
                BoundedCapacity = 16,
                CancellationToken = cancellationToken
            });

            var executionOptions = new ExecutionDataflowBlockOptions
            {
                BoundedCapacity = 24,
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            var projectBlock =
                new TransformManyBlock<IConnection, Tuple<IConnection, IProject>>(
                    async param =>
                        (await GetProjectsAsync(param, cancellationToken).ConfigureAwait(false)).Select(project => new Tuple<IConnection, IProject>(param, project)).ToArray(), executionOptions);

            var buildsBlock =
                new ActionBlock<Tuple<IConnection, IProject>>(
                    async param =>
                        await GetBuildsAsync(param.Item1, param.Item2, cancellationToken).ConfigureAwait(false), executionOptions);

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            projectBufferBlock.LinkTo(projectBlock, linkOptions);
            projectBlock.LinkTo(buildsBufferBlock, linkOptions);
            buildsBufferBlock.LinkTo(buildsBlock, linkOptions);

            foreach (var connection in connections)
            {
                await projectBufferBlock.SendAsync(connection, cancellationToken);
            }

            projectBufferBlock.Complete();

            await buildsBlock.Completion;
        }
    }
}