// <copyright file="ProjectToMonitorViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ProjectToMonitorViewModel" /> class.
    /// </summary>
    public class ProjectToMonitorViewModel : PropertyChangedBase
    {
        private bool _monitor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectToMonitorViewModel" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="monitor">Whether this project should be monitored.</param>
        public ProjectToMonitorViewModel(string name, string id, bool monitor)
        {
            Ensure.That(name).IsNotNullOrWhiteSpace();
            Ensure.That(id).IsNotNullOrWhiteSpace();

            Name = name;
            Id = id;
            _monitor = monitor;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this project should be monitored.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this project should be monitored; otherwise, <c>false</c>.
        /// </value>
        public bool Monitor
        {
            get
            {
                return _monitor;
            }

            set
            {
                _monitor = value;
                NotifyOfPropertyChange(() => Monitor);
            }
        }
    }
}
