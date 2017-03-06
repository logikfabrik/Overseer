﻿// <copyright file="BuildMonitor.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
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
    using Logging;

    /// <summary>
    /// The <see cref="BuildMonitor" /> class.
    /// </summary>
    public class BuildMonitor : IBuildMonitor
    {
        private readonly ILogService _logService;
        private readonly IDisposable _subscription;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildMonitor" /> class.
        /// </summary>
        /// <param name="connectionPool">The connection pool.</param>
        /// <param name="logService">The log service.</param>
        public BuildMonitor(IConnectionPool connectionPool, ILogService logService)
        {
            Ensure.That(connectionPool).IsNotNull();
            Ensure.That(logService).IsNotNull();

            _logService = logService;
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

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            var cancellationToken = _cancellationTokenSource.Token;

            // ReSharper disable once FunctionNeverReturns
            Task.Factory.StartNew(
                async () =>
                {
                    if (!value.Any())
                    {
                        return;
                    }

                    while (true)
                    {
                        try
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            await GetProjectsAndBuildsAsync(value, cancellationToken).ConfigureAwait(false);

                            // TODO: Read delay from configuration.
                            await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken).ConfigureAwait(false);
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            _logService.Log<BuildMonitor>(new LogEntry(LogEntryType.Error, "An error occurred while polling.", ex));
                        }
                    }
                }, cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Current);
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
            if (_isDisposed)
            {
                return;
            }

            // ReSharper disable once InvertIf
            if (disposing)
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();

                _subscription.Dispose();
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

        private async Task GetProjectsAndBuildsAsync(IEnumerable<IConnection> connections, CancellationToken cancellationToken)
        {
            var projectBufferBlock = new BufferBlock<IConnection>(new DataflowBlockOptions
            {
                BoundedCapacity = 4,
                CancellationToken = cancellationToken
            });

            var buildsBufferBlock = new BufferBlock<Tuple<IConnection, IProject>>(new DataflowBlockOptions
            {
                BoundedCapacity = 4,
                CancellationToken = cancellationToken
            });

            var executionOptions = new ExecutionDataflowBlockOptions
            {
                BoundedCapacity = 4,
                CancellationToken = cancellationToken
            };

            var projectBlock =
                new TransformManyBlock<IConnection, Tuple<IConnection, IProject>>(
                    async param =>
                        (await GetProjectsAsync(param, cancellationToken).ConfigureAwait(false)).Select(project => new Tuple<IConnection, IProject>(param, project)), executionOptions);

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
                projectBufferBlock.Post(connection);
            }

            projectBufferBlock.Complete();

            await buildsBlock.Completion;
        }

        private async Task<IEnumerable<IProject>> GetProjectsAsync(IConnection connection, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var projects = (await connection.GetProjectsAsync(cancellationToken).ConfigureAwait(false)).ToArray();

                OnConnectionProgressChanged(new BuildMonitorConnectionProgressEventArgs(connection.Settings.Id, projects));

                return projects;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                OnConnectionError(new BuildMonitorConnectionErrorEventArgs(connection.Settings.Id));

                _logService.Log<BuildMonitor>(new LogEntry(LogEntryType.Error, "An unexpected error occurred while polling projects.", ex));

                return new IProject[] { };
            }
        }

        private async Task GetBuildsAsync(IConnection connection, IProject project, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var builds = await connection.GetBuildsAsync(project, cancellationToken).ConfigureAwait(false);

                OnProjectProgressChanged(new BuildMonitorProjectProgressEventArgs(connection.Settings.Id, project, builds));
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                OnProjectError(new BuildMonitorProjectErrorEventArgs(connection.Settings.Id, project));

                _logService.Log<BuildMonitor>(new LogEntry(LogEntryType.Error, "An unexpected error occurred while polling builds.", ex));
            }
        }
    }
}