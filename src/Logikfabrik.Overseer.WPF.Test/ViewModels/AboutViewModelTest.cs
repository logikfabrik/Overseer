// <copyright file="AboutViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.ViewModels
{
    using Moq.AutoMock;
    using Shouldly;
    using WPF.ViewModels;
    using Xunit;

    public class AboutViewModelTest
    {
        [Fact]
        public void CanGetVersion()
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<ViewAboutViewModel>();

            model.Version.ShouldNotBeNull();
        }
    }
}
