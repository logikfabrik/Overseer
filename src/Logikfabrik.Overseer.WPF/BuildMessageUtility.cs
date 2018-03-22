// <copyright file="BuildMessageUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF
{
    using System.Text;

    /// <summary>
    /// The <see cref="BuildMessageUtility" /> class.
    /// </summary>
    public static class BuildMessageUtility
    {
        // TODO: Rename class or move function elsewhere.

        /// <summary>
        /// Gets the build name.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="versionNumber">The version number.</param>
        /// <returns>The build name.</returns>
        public static string GetBuildName(string projectName, string versionNumber)
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

            return builder.Length > 0 ? builder.ToString().Trim() : null;
        }
    }
}