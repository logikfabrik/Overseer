// <copyright file="BuildMessageUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EnsureThat;
    using Humanizer;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="BuildMessageUtility" /> class.
    /// </summary>
    public static class BuildMessageUtility
    {
        /// <summary>
        /// Gets the build name.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="build">The build.</param>
        /// <returns>The build name.</returns>
        public static string GetBuildName(IProject project, IBuild build)
        {
            Ensure.That(project).IsNotNull();
            Ensure.That(build).IsNotNull();

            return GetBuildName(project.Name, build.GetVersionNumber(), build.Branch);
        }

        /// <summary>
        /// Gets the build name.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="versionNumber">The version number.</param>
        /// <param name="branch">The branch.</param>
        /// <returns>The build name.</returns>
        public static string GetBuildName(string projectName, string versionNumber, string branch)
        {
            var builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(projectName))
            {
                builder.AppendFormat("{0} ", projectName);
            }

            if (!string.IsNullOrWhiteSpace(versionNumber))
            {
                builder.AppendFormat("{0} ", versionNumber);
            }

            if (!string.IsNullOrWhiteSpace(branch))
            {
                builder.AppendFormat("({0})", branch);
            }

            return builder.Length > 0 ? builder.ToString() : null;
        }

        /// <summary>
        /// Gets the build status message.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>The build status message.</returns>
        public static string GetBuildStatusMessage(BuildStatus? status)
        {
            return GetBuildStatusMessage(status, new Dictionary<string, string>());
        }

        /// <summary>
        /// Gets the build status message.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="parts">The parts.</param>
        /// <returns>The build status message.</returns>
        public static string GetBuildStatusMessage(BuildStatus? status, IDictionary<string, string> parts)
        {
            Ensure.That(parts).IsNotNull();

            if (!status.HasValue)
            {
                return null;
            }

            var messageParts = GetMessageParts(parts);

            return string.IsNullOrWhiteSpace(messageParts)
                ? $"Build {status.Value.Humanize().Transform(To.LowerCase)}"
                : $"Build {messageParts} {status.Value.Humanize().Transform(To.LowerCase)}";
        }

        /// <summary>
        /// Gets the build run time message.
        /// </summary>
        /// <param name="build">The build.</param>
        /// <returns>The build run time message.</returns>
        public static string GetBuildRunTimeMessage(IBuild build)
        {
            return GetBuildRunTimeMessage(build.Status, build.EndTime, build.GetRunTime());
        }

        /// <summary>
        /// Gets the build run time message.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="runTime">The run time.</param>
        /// <returns>The build run time message.</returns>
        public static string GetBuildRunTimeMessage(BuildStatus? status, DateTime? endTime, TimeSpan? runTime)
        {
            if (status.IsInProgress())
            {
                return !runTime.HasValue
                    ? status.Humanize()
                    : $"{status.Humanize()} for {runTime.Value.Humanize()}";
            }

            // ReSharper disable once InvertIf
            if (status.IsFinished())
            {
                if (!runTime.HasValue)
                {
                    return !endTime.HasValue
                        ? status.Humanize()
                        : $"{status.Humanize()} {endTime.Value.Humanize()}";
                }

                return !endTime.HasValue
                    ? $"{status.Humanize()} in {runTime.Value.Humanize()}"
                    : $"{status.Humanize()} in {runTime.Value.Humanize()}, {endTime.Value.Humanize()}";
            }

            return null;
        }

        /// <summary>
        /// Gets the success rate message.
        /// </summary>
        /// <param name="successRate">The success rate.</param>
        /// <returns>The success rate message.</returns>
        public static string GetSuccessRateMessage(double successRate)
        {
            return $"{successRate}% of recent builds were successful";
        }

        private static string GetMessageParts(IDictionary<string, string> parts)
        {
            var messageParts = parts.Where(part => !string.IsNullOrWhiteSpace(part.Value));

            return messageParts.Humanize(part => $"{part.Key?.Transform(To.LowerCase)} {part.Value}");
        }
    }
}