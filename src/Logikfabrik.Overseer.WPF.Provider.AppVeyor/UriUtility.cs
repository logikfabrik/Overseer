// <copyright file="UriUtility.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.AppVeyor
{
    using System;

    /// <summary>
    /// The <see cref="UriUtility" /> class.
    /// </summary>
    public static class UriUtility
    {
        public static Uri BaseUri => new Uri("https://ci.appveyor.com/");
    }
}
