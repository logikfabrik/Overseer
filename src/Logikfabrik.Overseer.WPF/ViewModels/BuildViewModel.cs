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
    public class BuildViewModel : PropertyChangedBase, IBuildViewModel
    {
        private readonly string _versionNumber;
        private string _name;
        private string _message;
        private BuildStatus? _status;
        private DateTime? _startTime;
        private DateTime? _endTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildViewModel" /> class.
        /// </summary>
        /// <param name="changeFactory">The change factory.</param>
        /// <param name="projectName">The project name.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="branch">The branch.</param>
        /// <param name="versionNumber">The version number.</param>
        /// <param name="requestedBy">The name of whoever requested the build.</param>
        /// <param name="changes">The changes.</param>
        /// <param name="status">The status.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="runTime">The run time.</param>
        public BuildViewModel(IChangeViewModelFactory changeFactory, string projectName, string id, string branch, string versionNumber, string requestedBy, IEnumerable<IChange> changes, BuildStatus? status, DateTime? startTime, DateTime? endTime, TimeSpan? runTime)
        {
            Ensure.That(changeFactory).IsNotNull();
            Ensure.That(id).IsNotNullOrWhiteSpace();
            Ensure.That(changes).IsNotNull();

            Id = id;
            Branch = branch;
            ShowBranch = !string.IsNullOrWhiteSpace(branch);
            _versionNumber = versionNumber;
            RequestedBy = requestedBy;
            ShowRequestedBy = !string.IsNullOrWhiteSpace(requestedBy);

            Changes = changes.Select(changeFactory.Create).ToArray();

            TryUpdate(projectName, status, startTime, endTime, runTime);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }

            private set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        /// <summary>
        /// Gets the name of whoever requested the build.
        /// </summary>
        /// <value>
        /// The name of whoever requested the build.
        /// </value>
        public string RequestedBy { get; }

        /// <summary>
        /// Gets a value indicating whether to show the name of whoever requested the build.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the name of whoever requested the build should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowRequestedBy { get; }

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; }

        /// <summary>
        /// Gets a value indicating whether to show the branch.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the branch should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowBranch { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                return _message;
            }

            private set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        /// <summary>
        /// Gets the status.
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

            private set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        /// <summary>
        /// Gets the start time.
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

            private set
            {
                _startTime = value;
                NotifyOfPropertyChange(() => StartTime);
            }
        }

        /// <summary>
        /// Gets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTime? EndTime
        {
            get
            {
                return _endTime;
            }

            private set
            {
                _endTime = value;
                NotifyOfPropertyChange(() => EndTime);
            }
        }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        public IEnumerable<IChangeViewModel> Changes { get; }

        /// <summary>
        /// Tries to update this instance.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="status">The status.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="runTime">The run time.</param>
        /// <returns><c>true</c> if this instance was updated; otherwise, <c>false</c>.</returns>
        public bool TryUpdate(string projectName, BuildStatus? status, DateTime? startTime, DateTime? endTime, TimeSpan? runTime)
        {
            var name = BuildMessageUtility.GetBuildName(projectName, _versionNumber, Branch);

            var isUpdated = false;

            if (Name != name)
            {
                Name = name;
                isUpdated = true;
            }

            var message = BuildMessageUtility.GetBuildRunTimeMessage(status, endTime, runTime);

            if (Message != message)
            {
                Message = message;
                Status = status;
                EndTime = endTime;
                isUpdated = true;
            }

            // ReSharper disable once InvertIf
            if (StartTime != startTime)
            {
                StartTime = startTime;
                isUpdated = true;
            }

            return isUpdated;
        }
    }
}
