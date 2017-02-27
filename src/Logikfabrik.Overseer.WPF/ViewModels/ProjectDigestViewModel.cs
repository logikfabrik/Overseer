// <copyright file="ProjectDigestViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Caliburn.Micro;
    using EnsureThat;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ProjectDigestViewModel" /> class. View model for CI project digest.
    /// </summary>
    public class ProjectDigestViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDigestViewModel" /> class.
        /// </summary>
        /// <param name="builds">The builds.</param>
        public ProjectDigestViewModel(IEnumerable<IBuild> builds)
        {
            Ensure.That(builds).IsNotNull();

            var finishedBuilds = builds.Where(build => build.IsFinished()).ToArray();

            SuccessRateMessage = BuildMessageUtility.GetSuccessRateMessage(finishedBuilds);

            var latestBuild = finishedBuilds.Where(build => build.IsFinished()).OrderByDescending(build => build.StartTime).FirstOrDefault();

            if (latestBuild == null)
            {
                return;
            }

            LatestBuildStatusMessage = BuildMessageUtility.GetBuildStatusMessage(latestBuild.Status);
            LatestBuildRunTimeMessage = BuildMessageUtility.GetBuildRunTimeMessage(latestBuild);
        }

        /// <summary>
        /// Gets the success rate message for the latest builds.
        /// </summary>
        /// <value>
        /// The success rate message for the latest builds.
        /// </value>
        public string SuccessRateMessage { get; }

        /// <summary>
        /// Gets the status message for the latest build.
        /// </summary>
        /// <value>
        /// The status message for the latest build.
        /// </value>
        public string LatestBuildStatusMessage { get; }

        /// <summary>
        /// Gets the run time message for the latest build.
        /// </summary>
        /// <value>
        /// The run time message for the latest build.
        /// </value>
        public string LatestBuildRunTimeMessage { get; }
    }
}