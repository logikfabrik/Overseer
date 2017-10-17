// <copyright file="MenuViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Test.ViewModels
{
    using Client.ViewModels;
    using Moq.AutoMock;
    using Shouldly;
    using WPF.ViewModels;
    using Xunit;

    public class MenuViewModelTest
    {
        [Fact]
        public void CanOpen()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsListViewModel>());

            var model = mocker.CreateInstance<MenuViewModel>();

            model.Open();

            model.IsExpanded.ShouldBeTrue();
        }

        [Fact]
        public void CanClose()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsListViewModel>());

            var model = mocker.CreateInstance<MenuViewModel>();

            model.Close();

            model.IsExpanded.ShouldBeFalse();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanGetIsExpanded(bool isExpanded)
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsListViewModel>());

            var model = mocker.CreateInstance<MenuViewModel>();

            model.IsExpanded = isExpanded;

            model.IsExpanded.ShouldBe(isExpanded);
        }
    }
}
