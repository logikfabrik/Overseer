// <copyright file="ViewModelNavigator.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Navigation
{
    using Caliburn.Micro;
    using ViewModels;

    /// <summary>
    /// The <see cref="ViewModelNavigator" /> class.
    /// </summary>
    public class ViewModelNavigator : Navigator<IViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelNavigator" /> class.
        /// </summary>
        /// <param name="conductor">The conductor.</param>
        public ViewModelNavigator(Conductor<IViewModel>.Collection.OneActive conductor)
            : base(conductor)
        {
        }

        protected override bool CanCloseItem(IViewModel item)
        {
            return !item.KeepAlive;
        }
    }
}
