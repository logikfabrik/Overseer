// <copyright file="ViewBuildViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.ViewModels
{
    using Moq;
    using Moq.AutoMock;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using WPF.ViewModels;
    using Xunit;

    public class ViewBuildViewModelTest
    {
        [Theory]
        [AutoData]
        public void CanGetId(string id)
        {
            var mocker = new AutoMocker();

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Id).Returns(id);

            mocker.Use(buildMock);

            var model = mocker.CreateInstance<ViewBuildViewModel>();

            model.Id.ShouldBe(id);
        }

        public void CanGetName()
        {
            // TODO: This unit test.
        }

        [Theory]
        [AutoData]
        public void CanGetVersionNumberByVersion(string version)
        {
            var mocker = new AutoMocker();

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Version).Returns(version);

            mocker.Use(buildMock);

            var model = mocker.CreateInstance<ViewBuildViewModel>();

            model.VersionNumber.ShouldBe(version);
        }

        [Theory]
        [AutoData]
        public void CanGetVersionNumberByNumber(string number)
        {
            var mocker = new AutoMocker();

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Number).Returns(number);

            mocker.Use(buildMock);

            var model = mocker.CreateInstance<ViewBuildViewModel>();

            model.VersionNumber.ShouldBe(number);
        }

        [Theory]
        [AutoData]
        public void CanGetRequestedBy(string requestedBy)
        {
            var mocker = new AutoMocker();

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.RequestedBy).Returns(requestedBy);

            mocker.Use(buildMock);

            var model = mocker.CreateInstance<ViewBuildViewModel>();

            model.RequestedBy.ShouldBe(requestedBy);
        }

        [Theory]
        [AutoData]
        public void CanGetBranch(string branch)
        {
            var mocker = new AutoMocker();

            var buildMock = new Mock<IBuild>();

            buildMock.Setup(m => m.Branch).Returns(branch);

            mocker.Use(buildMock);

            var model = mocker.CreateInstance<ViewBuildViewModel>();

            model.Branch.ShouldBe(branch);
        }

        public void CanGetMessage()
        {
            // TODO: This unit test.
        }
    }
}
