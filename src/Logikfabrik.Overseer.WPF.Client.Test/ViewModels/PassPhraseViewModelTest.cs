// <copyright file="PassPhraseViewModelTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Client.Test.ViewModels
{
    using Client.ViewModels;
    using Moq;
    using Ploeh.AutoFixture.Xunit2;
    using Settings;
    using Shouldly;
    using Xunit;

    public class PassPhraseViewModelTest
    {
        [Fact]
        public void IsInvalidWithoutPassPhrase()
        {
            var encrypterMock = new Mock<IConnectionSettingsEncrypter>();

            var model = new PassPhraseViewModel(encrypterMock.Object);

            model.IsValid.ShouldBeFalse();
        }

        [Theory]
        [AutoData]
        public void IsValidWithPassPhrase(string passPhrase)
        {
            var encrypterMock = new Mock<IConnectionSettingsEncrypter>();

            var model = new PassPhraseViewModel(encrypterMock.Object) { PassPhrase = passPhrase };

            model.IsValid.ShouldBeTrue();
        }
    }
}
