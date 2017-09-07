// <copyright file="ApiClient.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Caching;
    using EnsureThat;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Overseer.Extensions;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient : IApiClient, IDisposable
    {
        private readonly JsonMediaTypeFormatter _mediaTypeFormatter;
        private readonly string _cacheBaseKey;
        private Lazy<HttpClient> _httpClient;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public ApiClient(ConnectionSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            _mediaTypeFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter>
                    {
                        new IsoDateTimeConverter
                        {
                            DateTimeFormat = "yyyyMMdd'T'HHmmsszzz"
                        }
                    }
                }
            };

            var baseUri = BaseUriUtility.GetBaseUri(settings.Url, settings.Version, settings.AuthenticationType);

            switch (settings.AuthenticationType)
            {
                case AuthenticationType.GuestAuth:
                    _httpClient = new Lazy<HttpClient>(() => GetHttpClient(baseUri));
                    _cacheBaseKey = baseUri.ToString();

                    break;

                case AuthenticationType.HttpAuth:
                    _httpClient = new Lazy<HttpClient>(() => GetHttpClient(baseUri, settings.Username, settings.Password));
                    _cacheBaseKey = string.Concat(baseUri, settings.Username, settings.Password);

                    break;

                default:
                    throw new NotSupportedException($"Authentication type '{settings.AuthenticationType}' is not supported.");
            }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Projects> GetProjectsAsync(CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            cancellationToken.ThrowIfCancellationRequested();

            const string url = "projects";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Projects>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the builds.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="count">The count.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Builds> GetBuildsAsync(string projectId, int count, CancellationToken cancellationToken)
        {
            this.ThrowIfDisposed(_isDisposed);

            Ensure.That(projectId).IsNotNullOrWhiteSpace();
            Ensure.That(count).IsInRange(1, int.MaxValue);

            cancellationToken.ThrowIfCancellationRequested();

            var url = $"builds?locator=project:{projectId},count:{count}&fields=build(id,triggered(type,details,user),revisions,startDate,finishDate,status,number,lastChanges(change(id,version,username,date,comment)),testOccurrences,webUrl)";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.ThrowIfUnsuccessful();

                return await response.Content.ReadAsAsync<Builds>(new[] { _mediaTypeFormatter }, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Gets the cache key for this <see cref="ICacheable" /> instance.
        /// </summary>
        /// <returns>The cache key.</returns>
        public string GetCacheBaseKey()
        {
            return _cacheBaseKey;
        }

        /// <summary>
        /// Releases unmanaged and managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_httpClient != null)
                {
                    if (_httpClient.IsValueCreated)
                    {
                        _httpClient.Value.Dispose();
                    }

                    _httpClient = null;
                }
            }

            _isDisposed = true;
        }

        private static HttpClient GetHttpClient(Uri baseUri)
        {
            var client = new HttpClient { BaseAddress = baseUri };

            SetDefaultRequestHeaders(client);

            return client;
        }

        private static HttpClient GetHttpClient(Uri baseUri, string username, string password)
        {
            var client = new HttpClient { BaseAddress = baseUri };

            SetDefaultRequestHeaders(client);
            SetAuthRequestHeaders(client, username, password);

            return client;
        }

        private static void SetDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static void SetAuthRequestHeaders(HttpClient client, string username, string password)
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
    }
}