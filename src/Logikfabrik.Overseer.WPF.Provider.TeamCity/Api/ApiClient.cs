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
    using EnsureThat;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The <see cref="ApiClient" /> class.
    /// </summary>
    public class ApiClient : IDisposable
    {
        private readonly Lazy<HttpClient> _httpClient;
        private readonly JsonMediaTypeFormatter _mediaTypeFormatter;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public ApiClient(Uri baseUri, string username, string password)
            : this()
        {
            Ensure.That(baseUri).IsNotNull();
            Ensure.That(username).IsNotNullOrWhiteSpace();
            Ensure.That(password).IsNotNullOrWhiteSpace();

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(baseUri, username, password));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ApiClient(Uri baseUri)
            : this()
        {
            Ensure.That(baseUri).IsNotNull();

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient(baseUri));
        }

        private ApiClient()
        {
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
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Projects> GetProjectsAsync(CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            const string url = "projects";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Projects>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the build types.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<BuildTypes> GetBuildTypesAsync(string projectId, CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            Ensure.That(projectId).IsNotNullOrWhiteSpace();

            var url = $"buildTypes?locator=affectedProject:(id:{projectId})&fields=buildType(builds($locator(count:1),build(id)))";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<BuildTypes>(cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the build.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task.</returns>
        public async Task<Build> GetBuildAsync(string id, CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            Ensure.That(id).IsNotNullOrWhiteSpace();

            var url = $"builds/id:{id}";

            using (var response = await _httpClient.Value.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<Build>(new[] { _mediaTypeFormatter }, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

            // ReSharper disable once InvertIf
            if (disposing)
            {
                if (!_httpClient.IsValueCreated)
                {
                    return;
                }

                _httpClient.Value.CancelPendingRequests();
                _httpClient.Value.Dispose();
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
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
    }
}