// <copyright file="ViewModel.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels
{
    using Caliburn.Micro;

    /// <summary>
    /// The <see cref="ViewModel" /> class. Base class for view models intended to be accessed using a <see cref="Conductor{IViewModel}.Collection.OneActive" /> implementation.
    /// </summary>
    public abstract class ViewModel : ViewAware, IViewModel
    {
        private object _parent;
        private string _displayName;

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
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

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance should be kept alive on navigation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance should be kept alive on navigation; otherwise, <c>false</c>.
        /// </value>
        public bool KeepAlive { get; set; } = false;

        public void TryClose(bool? dialogResult = null)
        {
            // TODO: Inject
            PlatformProvider.Current.GetViewCloseAction(this, Views.Values, dialogResult).OnUIThread();
        }
    }
}