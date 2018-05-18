// <copyright file="AddConnectionViewExceptionToLocalizedStringConverterTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.Converters
{
    using System;
    using Shouldly;
    using WPF.Converters;
    using Xunit;

    public class AddConnectionViewExceptionToLocalizedStringConverterTest
    {
        [Fact]
        public void CanConvert()
        {
            var converter = new AddConnectionViewExceptionToLocalizedStringConverter();

            var value = converter.Convert(new Exception(), null, null, null);

            value.ShouldNotBeNull();
        }

        [Fact]
        public void CanNotConvert()
        {
            var converter = new AddConnectionViewExceptionToLocalizedStringConverter();

            var value = converter.Convert(null, null, null, null);

            value.ShouldBeNull();
        }

        [Fact]
        public void WillThrowIfConvertingBack()
        {
            var converter = new AddConnectionViewExceptionToLocalizedStringConverter();

            Assert.Throws<NotSupportedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
