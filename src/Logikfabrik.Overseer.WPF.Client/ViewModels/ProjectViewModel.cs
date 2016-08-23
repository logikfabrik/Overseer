// <copyright file="ProjectViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.ViewModels
{
    using System;
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ProjectViewModel" /> class.
    /// </summary>
    public class ProjectViewModel : PropertyChangedBase
    {
        private string _projectName;
        private DateTime? _lastBuildDate;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
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

        /// <summary>
        /// Gets or sets the last build date.
        /// </summary>
        /// <value>
        /// The last build date.
        /// </value>
        public DateTime? LastBuildDate
        {
            get
            {
                return _lastBuildDate;
            }

            set
            {
                _lastBuildDate = value;
                NotifyOfPropertyChange(() => LastBuildDate);
            }
        }
    }
}
