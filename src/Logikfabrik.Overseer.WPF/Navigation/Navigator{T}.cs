// <copyright file="Navigator{T}.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation
{
    using System.Diagnostics;
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
        public void Navigate(NavigationMessage message)
        {
            Ensure.That(message).IsNotNull();

            NavigateTo(message.To);

            Debug.WriteLine("Items:");

            foreach (var item in _conductor.Items)
            {
                Debug.WriteLine(item.GetType());
            }
        }

        /// <summary>
        /// Determines whether the specified item can be closed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if the specified item can be closed; otherwise, <c>false</c>.</returns>
        protected virtual bool CanCloseItem(T item)
        {
            var navigable = item as INavigable;

            return navigable == null || navigable.KeepAlive;
        }

        private void NavigateTo(NavigationTarget to)
        {
            var activeItem = _conductor.ActiveItem;

            if (CanCloseItem(activeItem))
            {
                CloseItem(activeItem);
            }

            if (to.Item != null)
            {
                ActivateItem(to.Item as T);
            }
            else
            {
                var item = IoC.GetInstance(to.ItemType, null) as T;

                ActivateItem(item);
            }
        }

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

            (_conductor.Items as BindableCollection<T>)?.Move(_conductor.Items.IndexOf(item), _conductor.Items.Count - 1);
        }
    }
}
