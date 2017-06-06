﻿// <copyright file="IBuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IBuildViewModel" /> interface.
    /// </summary>
    public interface IBuildViewModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the name of whoever requested the build.
        /// </summary>
        /// <value>
        /// The name of whoever requested the build.
        /// </value>
        string RequestedBy { get; }

        /// <summary>
        /// Gets a value indicating whether to show the name of whoever requested the build.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the name of whoever requested the build should be shown; otherwise, <c>false</c>.
        /// </value>
        bool ShowRequestedBy { get; }

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        string Branch { get; }

        /// <summary>
        /// Gets a value indicating whether to show the branch.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the branch should be shown; otherwise, <c>false</c>.
        /// </value>
        bool ShowBranch { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        BuildStatus? Status { get; }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        DateTime? StartTime { get; }

        /// <summary>
        /// Gets a value indicating whether to show the start time.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the start time should be shown; otherwise, <c>false</c>.
        /// </value>
        bool ShowStartTime { get; }

        /// <summary>
        /// Gets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        DateTime? EndTime { get; }

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        IEnumerable<IChangeViewModel> Changes { get; }

        /// <summary>
        /// Tries to update this instance.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="status">The status.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="runTime">The run time.</param>
        /// <returns><c>true</c> if this instance was updated; otherwise, <c>false</c>.</returns>
        bool TryUpdate(string projectName, BuildStatus? status, DateTime? startTime, DateTime? endTime, TimeSpan? runTime);
    }
}