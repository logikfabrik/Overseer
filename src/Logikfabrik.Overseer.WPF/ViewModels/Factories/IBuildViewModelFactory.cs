// <copyright file="IBuildViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IBuildViewModelFactory" /> interface.
    /// </summary>
    public interface IBuildViewModelFactory
    {
        /// <summary>
        /// Creates a view model.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="branch">The branch.</param>
        /// <param name="versionNumber">The version number.</param>
        /// <param name="requestedBy">The requested by.</param>
        /// <param name="changes">The changes.</param>
        /// <param name="status">The status.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="runTime">The run time.</param>
        /// <returns>A  view model.</returns>
        BuildViewModel CreateBuildViewModel(string projectName, string id, string branch, string versionNumber, string requestedBy, IEnumerable<IChange> changes, BuildStatus? status, DateTime? startTime, DateTime? endTime, TimeSpan? runTime);
    }
}
