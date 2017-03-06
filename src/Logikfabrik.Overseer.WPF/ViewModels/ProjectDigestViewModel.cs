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

            var successfulBuilds = finishedBuilds.Count(build => build.Status == BuildStatus.Succeeded);

            SuccessRate = finishedBuilds.Length == 0 ? 100 : (double)successfulBuilds / finishedBuilds.Length;

            var latestFinishedBuild = finishedBuilds.OrderByDescending(build => build.StartTime).FirstOrDefault();

            if (latestFinishedBuild == null)
            {
                return;
            }

            LatestBuildRunTimeMessage = BuildMessageUtility.GetBuildRunTimeMessage(latestFinishedBuild);
            ShowLatestBuildRunTimeMessage = true;
        }

        /// <summary>
        /// Gets the success rate.
        /// </summary>
        /// <value>
        /// The success rate.
        /// </value>
        public double SuccessRate { get; }

        /// <summary>
        /// Gets the run time message for the latest build.
        /// </summary>
        /// <value>
        /// The run time message for the latest build.
        /// </value>
        public string LatestBuildRunTimeMessage { get; }

        public bool ShowLatestBuildRunTimeMessage { get; }
    }
}