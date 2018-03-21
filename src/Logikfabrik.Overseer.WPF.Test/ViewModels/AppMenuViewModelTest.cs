// <copyright file="AppMenuViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.ViewModels
{
    using System;
    using Caliburn.Micro;
    using Moq;
    using Moq.AutoMock;
    using Shouldly;
    using WPF.Navigation;
    using WPF.ViewModels;
    using Xunit;

    public class AppMenuViewModelTest
    {
        [Fact]
        public void CanOpen()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsViewModel>());

            var model = mocker.CreateInstance<AppMenuViewModel>();

            model.Open();

            model.IsExpanded.ShouldBeTrue();
        }

        [Fact]
        public void CanClose()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsViewModel>());

            var model = mocker.CreateInstance<AppMenuViewModel>();

            model.Close();

            model.IsExpanded.ShouldBeFalse();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanGetIsExpanded(bool isExpanded)
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsViewModel>());

            var model = mocker.CreateInstance<AppMenuViewModel>();

            model.IsExpanded = isExpanded;

            model.IsExpanded.ShouldBe(isExpanded);
        }

        [Fact]
        public void CanGoToDashboard()
        {
            CanGoTo<ViewDashboardViewModel>(model => model.GoToDashboard());
        }

        [Fact]
        public void CanGoToConnections()
        {
            CanGoTo<ViewConnectionsViewModel>(model => model.GoToConnections());
        }

        [Fact]
        public void CanGoToAddConnection()
        {
            CanGoTo<NewConnectionViewModel>(model => model.GoToAddConnection());
        }

        [Fact]
        public void CanGoToSettings()
        {
            var mocker = new AutoMocker();

            var factoryMock = new Mock<IAppSettingsFactory>();

            factoryMock.Setup(m => m.Create()).Returns(new Mock<IAppSettings>().Object);

            mocker.Use(factoryMock);

            CanGoTo<EditSettingsViewModel>(model => model.GoToSettings(), mocker);
        }

        [Fact]
        public void CanGoToAbout()
        {
            CanGoTo<ViewAboutViewModel>(model => model.GoToAbout());
        }

        private static void CanGoTo<T>(Action<AppMenuViewModel> action, AutoMocker mocker = null)
            where T : class
        {
            mocker = mocker ?? new AutoMocker();

            // TODO: Break out to affected tests. Should not be in general method.
            mocker.Use(mocker.CreateInstance<FavoritesViewModel>());
            mocker.Use(mocker.CreateInstance<ConnectionsViewModel>());

            var model = mocker.CreateInstance<AppMenuViewModel>();

            var factoryMock = mocker.GetMock<INavigationMessageFactory<T>>();

            factoryMock.Setup(m => m.Create()).Returns(new NavigationMessage<T>(mocker.CreateInstance<T>()));

            action(model);

            var eventAggregatorMock = mocker.GetMock<IEventAggregator>();

            eventAggregatorMock.Verify(m => m.Publish(It.Is<INavigationMessage>(message => message.Item.GetType() == typeof(T)), It.IsAny<Action<System.Action>>()), Times.Once);
        }
    }
}
