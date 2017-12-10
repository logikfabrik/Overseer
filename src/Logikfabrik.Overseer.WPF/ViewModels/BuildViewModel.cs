// <copyright file="BuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class BuildViewModel : PropertyChangedBase, IBuildViewModel
    {
        private readonly Uri _webUrl;
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
        /// <param name="webUrl">The web URL.</param>
        public BuildViewModel(IChangeViewModelFactory changeFactory, string projectName, string id, string branch, string versionNumber, string requestedBy, IEnumerable<IChange> changes, BuildStatus? status, DateTime? startTime, DateTime? endTime, TimeSpan? runTime, Uri webUrl)
        {
            Ensure.That(changeFactory).IsNotNull();
            Ensure.That(id).IsNotNullOrWhiteSpace();
            Ensure.That(changes).IsNotNull();

            Id = id;
            Branch = branch;
            VersionNumber = versionNumber;
            _webUrl = webUrl;
            RequestedBy = requestedBy;

            Changes = changes.Select(changeFactory.Create).ToArray();

            TryUpdate(projectName, status, startTime, endTime, runTime);
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public string VersionNumber { get; }

        /// <inheritdoc />
        public string RequestedBy { get; }

        /// <inheritdoc />
        public string Branch { get; }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IEnumerable<IChangeViewModel> Changes { get; }

        /// <inheritdoc />
        public bool IsViewable => _webUrl != null;

        /// <inheritdoc />
        public void View()
        {
            if (!IsViewable)
            {
                return;
            }

            Process.Start(new ProcessStartInfo(_webUrl.AbsoluteUri));
        }

        /// <inheritdoc />
        public bool TryUpdate(string projectName, BuildStatus? status, DateTime? startTime, DateTime? endTime, TimeSpan? runTime)
        {
            var name = BuildMessageUtility.GetBuildName(projectName, VersionNumber);

            var isUpdated = false;

            if (Name != name)
            {
                Name = name;
                isUpdated = true;
            }

            var message = BuildMessageUtility.GetBuildRunTimeMessage(DateTime.UtcNow, status, endTime, runTime);

            if (Message != message)
            {
                Message = message;
                Status = status;
                EndTime = endTime;
                isUpdated = true;
            }

            if (StartTime != startTime)
            {
                StartTime = startTime;
                isUpdated = true;
            }

            // ReSharper disable once InvertIf
            if (EndTime != endTime)
            {
                EndTime = endTime;
                isUpdated = true;
            }

            return isUpdated;
        }
    }
}
