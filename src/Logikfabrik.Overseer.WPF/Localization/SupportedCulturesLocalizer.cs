// <copyright file="SupportedCulturesLocalizer.cs" company="Logikfabrik">
//   Copyright (c) 2016-2018 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Localization
{
    /// <summary>
    /// The <see cref="SupportedCulturesLocalizer" /> class.
    /// </summary>
    public static class SupportedCulturesLocalizer
    {
        public static string Localize(string cultureName)
        {
            switch (cultureName)
            {
                case SupportedCultures.EnUs:
                    return Properties.Resources.SupportedCultures_enUS_Name;

                case SupportedCultures.SvSe:
                    return Properties.Resources.SupportedCultures_svSE_Name;

                default:
                    return null;
            }
        }
    }
}
