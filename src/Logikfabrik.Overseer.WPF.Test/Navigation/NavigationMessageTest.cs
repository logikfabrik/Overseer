// <copyright file="NavigationMessageTest.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Test.Navigation
{
    using Shouldly;
    using WPF.Navigation;
    using Xunit;

    public class NavigationMessageTest
    {
        [Fact]
        public void CanGetItem()
        {
            var item = new object();

            var message = new NavigationMessage<object>(item);

            message.Item.ShouldBe(item);
        }
    }
}
