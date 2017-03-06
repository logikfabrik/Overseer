// <copyright file="ProjectDigestViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using EnsureThat;

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

            Forecast = new ProjectForecast(builds);

            SuccessRateMessage = BuildMessageUtility.GetSuccessRateMessage(Forecast.SuccessRate);

            var latestFinishedBuild = Forecast.LatestFinishedBuild;

            if (latestFinishedBuild == null)
            {
                return;
            }

            LatestBuildStatusMessage = BuildMessageUtility.GetBuildStatusMessage(latestFinishedBuild.Status);
            LatestBuildRunTimeMessage = BuildMessageUtility.GetBuildRunTimeMessage(latestFinishedBuild);
        }

        /// <summary>
        /// Gets the forecast.
        /// </summary>
        /// <value>
        /// The forecast.
        /// </value>
        public ProjectForecast Forecast { get; }

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