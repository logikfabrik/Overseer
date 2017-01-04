﻿// <copyright file="BuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Factories;
    using Humanizer;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class BuildViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildViewModel" /> class.
        /// </summary>
        /// <param name="changeFactory">The change factory.</param>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        public BuildViewModel(IChangeViewModelFactory changeFactory, IProject project, IBuild build)
        {
            Ensure.That(changeFactory).IsNotNull();
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            BuildId = build.Id;
            BuildName = GetBuildName(project, build);
            Message = GetMessage(build);
            Status = build.Status;
            Changes = build.Changes.Select(changeFactory.Create);
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
        public string BuildName { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        public IEnumerable<ChangeViewModel> Changes { get; }

        private static string GetBuildName(IProject project, IBuild build)
        {
            return $"{project.Name} {build.GetVersionNumber()} {(!string.IsNullOrWhiteSpace(build.Branch) ? $"({build.Branch})" : string.Empty)}";
        }

        private static string GetMessage(IBuild build)
        {
            var buildTime = build.GetBuildTime();

            string message = null;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (build.Status)
            {
                case BuildStatus.InProgress:
                    message = $"In progress {(build.Started.HasValue ? build.Started.Humanize() : string.Empty)} {(buildTime.HasValue ? $"since {buildTime.Value.Humanize()}" : string.Empty)}";
                    break;

                case BuildStatus.Stopped:
                case BuildStatus.Succeeded:
                case BuildStatus.Failed:
                    message = $"{build.Status} {(build.Started.HasValue ? build.Started.Humanize() : string.Empty)} {(buildTime.HasValue ? $"in {buildTime.Value.Humanize()}" : string.Empty)}";
                    break;
            }

            return !string.IsNullOrWhiteSpace(message)
                ? $"{message} {(!string.IsNullOrWhiteSpace(build.RequestedBy) ? $"for {build.RequestedBy}" : string.Empty)}"
                : null;
        }
    }
}
