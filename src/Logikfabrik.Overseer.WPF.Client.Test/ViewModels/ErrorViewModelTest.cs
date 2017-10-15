// <copyright file="ErrorViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Test.ViewModels
{
    using Client.ViewModels;
    using Moq.AutoMock;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class ErrorViewModelTest
    {
        [Theory]
        [AutoData]
        public void CanGetMessage(string message)
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<ErrorViewModel>();

            model.Message = message;

            model.Message.ShouldBe(message);
        }

        [Fact]
        public void CanGetIsExpanded()
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<ErrorViewModel>();

            model.IsExpanded = true;

            model.IsExpanded.ShouldBeTrue();
        }

        [Fact]
        public void CanClose()
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<ErrorViewModel>();

            model.Close();

            model.IsExpanded.ShouldBeFalse();
        }
    }
}
