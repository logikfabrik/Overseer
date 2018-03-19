// <copyright file="IEditFavoriteViewModelFactory.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;

    public interface IEditFavoriteViewModelFactory
    {
        EditFavoriteViewModel Create(Guid settingsId, string projectId);
    }
}
