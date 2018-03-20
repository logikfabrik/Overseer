// <copyright file="ViewFavoriteViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using System;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ViewFavoriteViewModel" /> class.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class ViewFavoriteViewModel : PropertyChangedBase, IViewFavoriteViewModel
    {
        private string _projectName;
        private bool _isBusy;
        private bool _isErrored;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewFavoriteViewModel" /> class.
        /// </summary>
        /// <param name="settingsId">The settings identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        // ReSharper disable once InheritdocConsiderUsage
        public ViewFavoriteViewModel(Guid settingsId, string projectId)
        {
            Ensure.That(settingsId).IsNotEmpty();
            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            SettingsId = settingsId;
            ProjectId = projectId;

            _isBusy = true;
            _isErrored = false;
        }

        /// <inheritdoc />
        public Guid SettingsId { get; }

        /// <inheritdoc />
        public string ProjectId { get; }

        /// <inheritdoc />
        public string ProjectName
        {
            get
            {
                return _projectName;

            }

            set
            {
                _projectName = value;
                NotifyOfPropertyChange(() => ProjectName);
            }
        }

        /// <inheritdoc />
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        /// <inheritdoc />
        public bool IsErrored
        {
            get
            {
                return _isErrored;
            }

            set
            {
                _isErrored = value;
                NotifyOfPropertyChange(() => IsErrored);
            }
        }

        /// <inheritdoc />
        public void TryUpdate(string projectName, bool isErrored)
        {
            ProjectName = projectName;
            IsErrored = isErrored;
            IsBusy = false;
        }
    }
}
