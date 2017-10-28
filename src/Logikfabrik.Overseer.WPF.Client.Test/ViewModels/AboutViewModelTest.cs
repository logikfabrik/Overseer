// <copyright file="AboutViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Test.ViewModels
{
    using Client.ViewModels;
    using Moq.AutoMock;
    using Shouldly;
    using Xunit;

    public class AboutViewModelTest
    {
        [Fact]
        public void CanGetVersion()
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<AboutViewModel>();

            model.Version.ShouldNotBeNull();
        }
    }
}
