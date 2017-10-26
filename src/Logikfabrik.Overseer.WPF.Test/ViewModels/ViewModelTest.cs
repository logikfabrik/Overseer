// <copyright file="ViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.ViewModels
{
    using Moq;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using WPF.ViewModels;
    using Xunit;

    public class ViewModelTest
    {
        [Theory]
        [AutoData]
        public void CanSetParent(object parent)
        {
            var model = new Mock<ViewModel>().Object;

            model.Parent = parent;

            model.Parent.ShouldBe(parent);
        }

        [Theory]
        [AutoData]
        public void CanSetDisplayName(string displayName)
        {
            var model = new Mock<ViewModel>().Object;

            model.DisplayName = displayName;

            model.DisplayName.ShouldBe(displayName);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanSetKeepAlive(bool keepAlive)
        {
            var model = new Mock<ViewModel>().Object;

            model.KeepAlive = keepAlive;

            model.KeepAlive.ShouldBe(keepAlive);
        }
    }
}
