// <copyright file="BuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using Humanizer;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class BuildViewModel : PropertyChangedBase
    {
        private readonly string _requestedBy;
        private string _projectName;
        private string _versionNumber;
        private string _branch;
        private BuildStatus? _status;
        private TimeSpan? _buildTime;
        private DateTime? _started;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildViewModel" /> class.
        /// </summary>
        /// <param name="changeFactory">The change factory.</param>
        /// <param name="buildId">The build identifier.</param>
        /// <param name="requestedBy">The name of whoever requested the build.</param>
        /// <param name="changes">The changes.</param>
        public BuildViewModel(IChangeViewModelFactory changeFactory, string buildId, string requestedBy, IEnumerable<IChange> changes)
        {
            Ensure.That(changeFactory).IsNotNull();
            Ensure.That(buildId).IsNotNullOrWhiteSpace();
            Ensure.That(changes).IsNotNull();

            BuildId = buildId;
            _requestedBy = requestedBy;
            Changes = changes.Select(changeFactory.Create);
        }

        /// <summary>
        /// Gets the build identifier.
        /// </summary>
        /// <value>
        /// The build identifier.
        /// </value>
        public string BuildId { get; }

        /// <summary>
        /// Gets the build name.
        /// </summary>
        /// <value>
        /// The build name.
        /// </value>
        public string BuildName { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;

                NotifyOfPropertyChange(() => Status);

                UpdateMessage();
            }
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        public IEnumerable<ChangeViewModel> Changes { get; }

        /// <summary>
        /// Sets the project name.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        public void SetProjectName(string projectName)
        {
            _projectName = projectName;

            UpdateBuildName();
        }

        /// <summary>
        /// Sets the version number.
        /// </summary>
        /// <param name="versionNumber">The version number.</param>
        public void SetVersionNumber(string versionNumber)
        {
            _versionNumber = versionNumber;

            UpdateBuildName();
        }

        /// <summary>
        /// Sets the branch.
        /// </summary>
        /// <param name="branch">The branch.</param>
        public void SetBranch(string branch)
        {
            _branch = branch;

            UpdateBuildName();
        }

        /// <summary>
        /// Sets the build time.
        /// </summary>
        /// <param name="buildTime">The build time.</param>
        public void SetBuildTime(TimeSpan? buildTime)
        {
            _buildTime = buildTime;

            UpdateMessage();
        }

        /// <summary>
        /// Sets the started date.
        /// </summary>
        /// <param name="started">The started date.</param>
        public void SetStarted(DateTime? started)
        {
            _started = started;

            UpdateMessage();
        }

        private void UpdateBuildName()
        {
            BuildName = $"{_projectName} {_versionNumber} {(!string.IsNullOrWhiteSpace(_branch) ? $"({_branch})" : string.Empty)}";

            NotifyOfPropertyChange(() => BuildName);
        }

        private void UpdateMessage()
        {
            string message = null;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_status)
            {
                case BuildStatus.InProgress:
                    message = $"In progress {(_started.HasValue ? _started.Humanize() : string.Empty)} {(_buildTime.HasValue ? $"since {_buildTime.Value.Humanize()}" : string.Empty)}";
                    break;

                case BuildStatus.Stopped:
                case BuildStatus.Succeeded:
                case BuildStatus.Failed:
                    message = $"{_status} {(_started.HasValue ? _started.Humanize() : string.Empty)} {(_buildTime.HasValue ? $"in {_buildTime.Value.Humanize()}" : string.Empty)}";
                    break;
            }

            Message = !string.IsNullOrWhiteSpace(message)
                ? $"{message} {(!string.IsNullOrWhiteSpace(_requestedBy) ? $"for {_requestedBy}" : string.Empty)}"
                : null;

            NotifyOfPropertyChange(() => Message);
        }
    }
}
