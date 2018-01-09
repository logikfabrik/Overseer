// <copyright file="AppErrorViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Test.ViewModels
{
    using Client.ViewModels;
    using Moq.AutoMock;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class AppErrorViewModelTest
    {
        [Theory]
        [AutoData]
        public void CanGetMessage(string message)
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<AppErrorViewModel>();

            model.Message = message;

            model.Message.ShouldBe(message);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanGetIsExpanded(bool isExpanded)
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<AppErrorViewModel>();

            model.IsExpanded = isExpanded;

            model.IsExpanded.ShouldBe(isExpanded);
        }

        [Fact]
        public void CanClose()
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<AppErrorViewModel>();

            model.Collapse();

            model.IsExpanded.ShouldBeFalse();
        }
    }
}
