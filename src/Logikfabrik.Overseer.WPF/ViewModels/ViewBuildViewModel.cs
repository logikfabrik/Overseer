// <copyright file="ViewBuildViewModel.cs" company="Logikfabrik">
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
    using Localization;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ViewBuildViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewBuildViewModel : PropertyChangedBase, IViewBuildViewModel
    {
        private readonly Uri _webUrl;
        private string _name;
        private string _statusMessage;
        private BuildStatus? _status;
        private DateTime? _startTime;
        private DateTime? _endTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBuildViewModel" /> class.
        /// </summary>
        /// <param name="viewChangeViewModelFactory">The view change view model factory.</param>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ViewBuildViewModel(IViewChangeViewModelFactory viewChangeViewModelFactory, IProject project, IBuild build)
        {
            Ensure.That(viewChangeViewModelFactory).IsNotNull();
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            Id = build.Id;
            Branch = build.Branch;
            VersionNumber = build.VersionNumber();
            _webUrl = build.WebUrl;
            RequestedBy = build.RequestedBy;
            Changes = build.Changes.Select(viewChangeViewModelFactory.Create).ToArray();

            Update(project, build);
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
        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }

            private set
            {
                _statusMessage = value;
                NotifyOfPropertyChange(() => StatusMessage);
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
        public IEnumerable<IViewChangeViewModel> Changes { get; }

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
        public void Update(IProject project, IBuild build)
        {
            var name = build.Name(project.Name);

            if (Name != name)
            {
                Name = name;
            }

            var statusMessage = BuildStatusMessageLocalizer.Localize(build.Status, build.RunTime(), build.EndTime);

            if (StatusMessage != statusMessage)
            {
                StatusMessage = statusMessage;
                Status = build.Status;
                EndTime = build.EndTime;
            }

            if (StartTime != build.StartTime)
            {
                StartTime = build.StartTime;
            }

            EndTime = build.EndTime;
        }
    }
}
