namespace Logikfabrik.Overseer.WPF.Localization
{
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
