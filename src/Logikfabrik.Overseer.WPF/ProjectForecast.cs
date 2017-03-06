// <copyright file="ProjectForecast.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ProjectForecast" /> class.
    /// </summary>
    public class ProjectForecast
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectForecast" /> class.
        /// </summary>
        /// <param name="builds">The builds.</param>
        public ProjectForecast(IEnumerable<IBuild> builds)
        {
            Ensure.That(builds).IsNotNull();

            var finishedBuilds = builds.Where(build => build.IsFinished()).ToArray();

            LatestFinishedBuild = finishedBuilds.OrderByDescending(build => build.StartTime).FirstOrDefault();
            FinishedBuilds = finishedBuilds.Length;
            SuccessfulBuilds = finishedBuilds.Count(build => build.Status == BuildStatus.Succeeded);
            SuccessRate = FinishedBuilds == 0 ? 100 : (double)SuccessfulBuilds / FinishedBuilds * 100;
            Forecast = GetForecast(SuccessRate);
        }

        /// <summary>
        /// Gets the success rate.
        /// </summary>
        /// <value>
        /// The success rate.
        /// </value>
        public double SuccessRate { get; }

        /// <summary>
        /// Gets the latest finished build.
        /// </summary>
        /// <value>
        /// The latest finished build.
        /// </value>
        public IBuild LatestFinishedBuild { get; }

        /// <summary>
        /// Gets the number of finished builds.
        /// </summary>
        /// <value>
        /// The number finished builds.
        /// </value>
        public int FinishedBuilds { get; }

        /// <summary>
        /// Gets the number of successful builds.
        /// </summary>
        /// <value>
        /// The number of successful builds.
        /// </value>
        public int SuccessfulBuilds { get; }

        /// <summary>
        /// Gets the forecast.
        /// </summary>
        /// <value>
        /// The forecast.
        /// </value>
        public Forecast Forecast { get; }

        private static Forecast GetForecast(double successRate)
        {
            if (successRate >= 0 && successRate <= 20)
            {
                return Forecast.CloudyWithLightning;
            }

            if (successRate > 20 && successRate <= 40)
            {
                return Forecast.CloudyWithRain;
            }

            if (successRate > 40 && successRate <= 60)
            {
                return Forecast.Cloudy;
            }

            if (successRate > 60 && successRate <= 80)
            {
                return Forecast.PartlyCloudy;
            }

            return Forecast.Fair;
        }
    }
}