// <copyright file="BuildViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.DesignTime
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="BuildViewModel" /> class.
    /// </summary>
    public class BuildViewModel : IBuildViewModel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; } = "1234";

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; } = "1234";

        /// <summary>
        /// Gets the name of whoever requested the build.
        /// </summary>
        /// <value>
        /// The name of whoever requested the build.
        /// </value>
        public string RequestedBy { get; } = "John Doe";

        /// <summary>
        /// Gets a value indicating whether to show the name of whoever requested the build.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the name of whoever requested the build should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowRequestedBy { get; } = true;

        /// <summary>
        /// Gets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; } = "master";

        /// <summary>
        /// Gets a value indicating whether to show the branch.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the branch should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowBranch { get; } = true;

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; } = "Message goes here";

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BuildStatus? Status { get; } = BuildStatus.Succeeded;

        /// <summary>
        /// Gets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime? StartTime { get; } = DateTime.Now;

        /// <summary>
        /// Gets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTime? EndTime { get; } = DateTime.Now;

        /// <summary>
        /// Gets a value indicating whether to show the start time.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the start time should be shown; otherwise, <c>false</c>.
        /// </value>
        public bool ShowStartTime { get; } = true;

        /// <summary>
        /// Gets the changes.
        /// </summary>
        /// <value>
        /// The changes.
        /// </value>
        public IEnumerable<IChangeViewModel> Changes { get; } = new[] { new ChangeViewModel(), new ChangeViewModel(), new ChangeViewModel() };

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
            throw new NotImplementedException();
        }
    }
}
