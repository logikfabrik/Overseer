// <copyright file="EditConnectionViewExceptionToLocalizedStringConverterTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.Converters
{
    using System;
    using Shouldly;
    using WPF.Converters;
    using Xunit;

    public class EditConnectionViewExceptionToLocalizedStringConverterTest
    {
        [Fact]
        public void CanConvert()
        {
            var converter = new EditConnectionViewExceptionToLocalizedStringConverter();

            var value = converter.Convert(new Exception(), null, null, null);

            value.ShouldNotBeNull();
        }

        [Fact]
        public void CanNotConvert()
        {
            var converter = new EditConnectionViewExceptionToLocalizedStringConverter();

            var value = converter.Convert(null, null, null, null);

            value.ShouldBeNull();
        }

        [Fact]
        public void WillThrowIfConvertingBack()
        {
            var converter = new EditConnectionViewExceptionToLocalizedStringConverter();

            Assert.Throws<NotSupportedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
