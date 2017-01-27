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
        private TimeSpan? _runTime;
        private DateTime? _startTime;

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
        /// Gets the build run time message.
        /// </summary>
        /// <value>
        /// The build run time message.
        /// </value>
        public string BuildRunTimeMessage { get; private set; }

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

                UpdateBuildRunTimeMessage();
            }
        }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime? StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value;

                NotifyOfPropertyChange(() => StartTime);

                UpdateBuildRunTimeMessage();
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
        /// Sets the run time.
        /// </summary>
        /// <param name="runTime">The run time.</param>
        public void SetRunTime(TimeSpan? runTime)
        {
            _runTime = runTime;

            UpdateBuildRunTimeMessage();
        }

        private void UpdateBuildName()
        {
            BuildName = BuildMessageUtil.GetBuildName(_projectName, _versionNumber, _branch);
            NotifyOfPropertyChange(() => BuildName);
        }

        private void UpdateBuildRunTimeMessage()
        {
            BuildRunTimeMessage = BuildMessageUtil.GetBuildRunTimeMessage(_status, _runTime, _startTime);
            NotifyOfPropertyChange(() => BuildRunTimeMessage);
        }
    }
}
