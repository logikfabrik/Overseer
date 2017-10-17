// <copyright file="PassPhraseViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Test.ViewModels
{
    using Client.ViewModels;
    using Moq.AutoMock;
    using Ploeh.AutoFixture.Xunit2;
    using Shouldly;
    using Xunit;

    public class PassPhraseViewModelTest
    {
        [Fact]
        public void IsInvalidWithoutPassPhrase()
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<PassPhraseViewModel>();

            model.IsValid.ShouldBeFalse();
        }

        [Theory]
        [AutoData]
        public void IsValidWithPassPhrase(string passPhrase)
        {
            var mocker = new AutoMocker();

            var model = mocker.CreateInstance<PassPhraseViewModel>();

            model.PassPhrase = passPhrase;

            model.IsValid.ShouldBeTrue();
        }
    }
}
