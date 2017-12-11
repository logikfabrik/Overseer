﻿// <copyright file="MenuViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Test.ViewModels
{
    using Caliburn.Micro;
    using Client.ViewModels;
    using Moq;
    using Moq.AutoMock;
    using Navigation;
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

        [Fact]
        public void CanGoToDashboard()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsListViewModel>());

            var model = mocker.CreateInstance<MenuViewModel>();

            model.GoToDashboard();

            var eventAggregatorMock = mocker.GetMock<IEventAggregator>();

            eventAggregatorMock.Verify(m => m.Publish(It.Is<NavigationMessage>(message => message.ItemType == typeof(DashboardViewModel)), It.IsAny<System.Action<System.Action>>()), Times.Once);
        }

        [Fact]
        public void CanGoToConnections()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsListViewModel>());

            var model = mocker.CreateInstance<MenuViewModel>();

            model.GoToConnections();

            var eventAggregatorMock = mocker.GetMock<IEventAggregator>();

            eventAggregatorMock.Verify(m => m.Publish(It.Is<NavigationMessage>(message => message.ItemType == typeof(ConnectionsViewModel)), It.IsAny<System.Action<System.Action>>()), Times.Once);
        }

        [Fact]
        public void CanGoToAddConnection()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsListViewModel>());

            var model = mocker.CreateInstance<MenuViewModel>();

            model.GoToAddConnection();

            var eventAggregatorMock = mocker.GetMock<IEventAggregator>();

            eventAggregatorMock.Verify(m => m.Publish(It.Is<NavigationMessage>(message => message.ItemType == typeof(BuildProvidersViewModel)), It.IsAny<System.Action<System.Action>>()), Times.Once);
        }

        [Fact]
        public void CanGoToSettings()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsListViewModel>());

            var model = mocker.CreateInstance<MenuViewModel>();

            model.GoToSettings();

            var eventAggregatorMock = mocker.GetMock<IEventAggregator>();

            eventAggregatorMock.Verify(m => m.Publish(It.Is<NavigationMessage>(message => message.ItemType == typeof(EditSettingsViewModel)), It.IsAny<System.Action<System.Action>>()), Times.Once);
        }

        [Fact]
        public void CanGoToAbout()
        {
            var mocker = new AutoMocker();

            mocker.Use(mocker.CreateInstance<ConnectionsListViewModel>());

            var model = mocker.CreateInstance<MenuViewModel>();

            model.GoToAbout();

            var eventAggregatorMock = mocker.GetMock<IEventAggregator>();

            eventAggregatorMock.Verify(m => m.Publish(It.Is<NavigationMessage>(message => message.ItemType == typeof(AboutViewModel)), It.IsAny<System.Action<System.Action>>()), Times.Once);
        }
    }
}
