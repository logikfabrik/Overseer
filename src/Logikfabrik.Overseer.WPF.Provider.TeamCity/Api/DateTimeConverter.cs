// <copyright file="DateTimeConverter.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="DateTimeConverter" /> class.
    /// </summary>
    public class DateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Do nothing.
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // 20161213T194638+0300
            var value = existingValue?.ToString();

            DateTime result;

            if (DateTime.TryParseExact(value, "", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result))
            {
                return result;
            }

            throw new Exception("TODO");
        }

        public override bool CanConvert(Type objectType)
        {
            var supported = new[] { typeof(DateTime), typeof(DateTime?) };

            return supported.Contains(objectType);
        }
    }
}
