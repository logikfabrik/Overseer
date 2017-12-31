// <copyright file="ViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="ViewModel" /> class. Base class for view models intended to be accessed using a <see cref="Conductor{IViewModel}.Collection.OneActive" /> implementation.
    /// </summary>
    public abstract class ViewModel : ViewAware, IViewModel
    {
        private readonly IPlatformProvider _platformProvider;

        private object _parent;
        private string _displayName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel" /> class.
        /// </summary>
        /// <param name="platformProvider">The platform provider.</param>
        protected ViewModel(IPlatformProvider platformProvider)
        {
            Ensure.That(platformProvider).IsNotNull();

            _platformProvider = platformProvider;
        }

        /// <inheritdoc />
        public object Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
                NotifyOfPropertyChange(() => Parent);
            }
        }

        /// <inheritdoc />
        public string DisplayName
        {
            get
            {
                return _displayName;
            }

            set
            {
                _displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        /// <inheritdoc />
        public bool KeepAlive { get; set; } = false;

        /// <inheritdoc/>
        public void TryClose(bool? dialogResult = null)
        {
            _platformProvider.GetViewCloseAction(this, Views.Values, dialogResult).OnUIThread();
        }
    }
}