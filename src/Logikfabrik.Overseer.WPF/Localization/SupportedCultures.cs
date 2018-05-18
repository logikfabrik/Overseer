// <copyright file="SupportedCultures.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
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
        public const string EnUs = "en-US";
        public const string SvSe = "sv-SE";

        /// <summary>
        /// Gets the culture names.
        /// </summary>
        /// <value>
        /// The culture names.
        /// </value>
        public static IEnumerable<string> CultureNames => new[] { EnUs, SvSe };

        /// <summary>
        /// Gets the cultures.
        /// </summary>
        /// <value>
        /// The cultures.
        /// </value>
        public static IEnumerable<CultureInfo> Cultures => CultureNames.Select(CultureInfo.GetCultureInfo);
    }
}
