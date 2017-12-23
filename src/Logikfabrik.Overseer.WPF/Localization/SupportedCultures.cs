// <copyright file="SupportedCultures.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Localization
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// The <see cref="SupportedCultures" /> class.
    /// </summary>
    public static class SupportedCultures
    {
        public static IEnumerable<string> CultureNames => new[] { "en-US", "sv-SE" };

        public static IEnumerable<CultureInfo> Cultures => CultureNames.Select(CultureInfo.GetCultureInfo);
    }
}
