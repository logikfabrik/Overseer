// <copyright file="ViewModelExtensions.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The <see cref="ViewModelExtensions" /> class.
    /// </summary>
    public static class ViewModelExtensions
    {
        public static IEnumerable<T> GetViewModels<T>(this IViewModel viewModel)
            where T : IViewModel
        {
            var conductor = viewModel.GetConductor<IViewModel>();

            return conductor?.Items.OfType<T>() ?? new T[] { };
        }
    }
}
