// <copyright file="ViewFavoriteViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ViewFavoriteViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewFavoriteViewModel : IViewFavoriteViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewFavoriteViewModel" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        public ViewFavoriteViewModel(Guid settingsId, string projectId)
        {
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            SettingsId = settingsId;
            ProjectId = projectId;
        }

        /// <inheritdoc />
        public Guid SettingsId { get; }

        /// <inheritdoc />
        public string ProjectId { get; }
    }
}
