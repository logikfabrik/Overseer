// <copyright file="ApiSettings.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Api
{
    using System.Net;
    using System.Net.Http;

    public class ApiSettings
    {
        public string ProxyUrl { get; set; }

        public string ProxyUsername { get; set; }

        public string ProxyPassword { get; set; }

        public HttpClientHandler GetHandler()
        {
            var proxy = new WebProxy(ProxyUrl, false);

            var handler = new HttpClientHandler
            {
                Proxy = proxy
            };

            if (!HasCredentials())
            {
                return handler;
            }

            proxy.Credentials = new NetworkCredential(ProxyUsername, ProxyPassword);
            proxy.UseDefaultCredentials = false;

            handler.PreAuthenticate = true;
            handler.UseDefaultCredentials = false;

            return handler;
        }

        private bool HasCredentials()
        {
            return ProxyUsername != null && ProxyPassword != null;
        }
    }
}