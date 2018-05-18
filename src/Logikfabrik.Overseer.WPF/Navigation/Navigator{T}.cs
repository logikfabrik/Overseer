// <copyright file="Navigator{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation
{
    using System.Collections.ObjectModel;
    using Caliburn.Micro;
    using EnsureThat;

    /// <summary>
    /// The <see cref="Navigator{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    public class Navigator<T>
        where T : class
    {
        private readonly Conductor<T>.Collection.OneActive _conductor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Navigator{T}" /> class.
        /// </summary>
        /// <param name="conductor">The conductor.</param>
        public Navigator(Conductor<T>.Collection.OneActive conductor)
        {
            Ensure.That(conductor).IsNotNull();

            _conductor = conductor;
        }

        /// <summary>
        /// Navigates the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Navigate(INavigationMessage message)
        {
            Ensure.That(message).IsNotNull();

            NavigateTo(message);
        }

        /// <summary>
        /// Determines whether the specified item can be closed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if the specified item can be closed; otherwise, <c>false</c>.</returns>
        private static bool CanCloseItem(T item)
        {
            var navigable = item as IKeepAlive;

            return navigable == null || !navigable.KeepAlive;
        }

        private void NavigateTo(INavigationMessage message)
        {
            var activeItem = _conductor.ActiveItem;

            if (CanCloseItem(activeItem))
            {
                CloseItem(activeItem);
            }

            ActivateItem(message.Item as T);
        }

        /// <summary>
        /// Closes the specified item, if possible.
        /// </summary>
        /// <param name="item">The item to close.</param>
        private void CloseItem(T item)
        {
            if (item == null)
            {
                return;
            }

            if (!CanCloseItem(item))
            {
                return;
            }

            _conductor.CloseItem(item);
        }

        private void ActivateItem(T item)
        {
            if (item == null)
            {
                return;
            }

            _conductor.ActivateItem(item);

            (_conductor.Items as ObservableCollection<T>)?.Move(_conductor.Items.IndexOf(item), _conductor.Items.Count - 1);
        }
    }
}
