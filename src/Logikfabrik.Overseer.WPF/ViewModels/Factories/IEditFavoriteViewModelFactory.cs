namespace Logikfabrik.Overseer.WPF.ViewModels.Factories
{
    using System;

    public interface IEditFavoriteViewModelFactory
    {
        EditFavoriteViewModel Create(Guid settingsId, string projectId);
    }
}
