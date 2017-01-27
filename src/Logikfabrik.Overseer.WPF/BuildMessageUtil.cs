// <copyright file="BuildMessageUtil.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Humanizer;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="BuildMessageUtil" /> class.
    /// </summary>
    public static class BuildMessageUtil
    {
        /// <summary>
        /// Gets the name of the specified build.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        /// <returns>The name of the specified build.</returns>
        public static string GetBuildName(IProject project, IBuild build)
        {
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            return GetBuildName(project.Name, build.GetVersionNumber(), build.Branch);
        }

        public static string GetBuildName(string projectName, string versionNumber, string branch)
        {
            return $"{projectName} {versionNumber} {(!string.IsNullOrWhiteSpace(branch) ? $"({branch})" : string.Empty)}";
        }

        public static string GetBuildStatusMessage(BuildStatus? status)
        {
            return GetBuildStatusMessage(status, new Dictionary<string, string>());
        }

        public static string GetBuildStatusMessage(BuildStatus? status, IDictionary<string, string> parts)
        {
            Ensure.That(parts).IsNotNull();

            return $"Build {GetMessageParts(parts)} {status.Humanize().Transform(To.LowerCase)}";
        }

        public static string GetBuildRunTimeMessage(IBuild build)
        {
            return GetBuildRunTimeMessage(build.Status, build.GetRunTime(), build.StartTime);
        }

        public static string GetBuildRunTimeMessage(BuildStatus? status, TimeSpan? runTime, DateTime? startTime)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (status)
            {
                case BuildStatus.InProgress:
                    return $"{status.Humanize()} for {runTime?.Humanize()}";

                case BuildStatus.Stopped:
                case BuildStatus.Succeeded:
                case BuildStatus.Failed:
                    return $"{status.Humanize()} in {runTime?.Humanize()}, {startTime?.Humanize()}";
                default:
                    return null;
            }
        }

        public static string GetSuccessRateMessage(IEnumerable<IBuild> builds)
        {
            Ensure.That(builds).IsNotNull();

            return $"{GetSuccessRate(builds)}% of recent builds were successful";
        }

        private static double GetSuccessRate(IEnumerable<IBuild> builds)
        {
            var finishedBuilds = builds.Where(build => build.IsFinished()).ToArray();

            var numberOfBuilds = finishedBuilds.Length;

            if (numberOfBuilds < 1)
            {
                return 0;
            }

            var numberOfSuccessfulBuilds = finishedBuilds.Count(build => build.Status == BuildStatus.Succeeded);

            return (double)numberOfSuccessfulBuilds / numberOfBuilds * 100;
        }

        private static string GetMessageParts(IDictionary<string, string> parts)
        {
            return parts.Humanize(part => $"{part.Key?.Transform(To.LowerCase)} {part.Value}");
        }
    }
}
