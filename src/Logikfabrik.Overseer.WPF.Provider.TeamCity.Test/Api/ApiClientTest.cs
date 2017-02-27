// <copyright file="ApiClientTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.WPF.Provider.TeamCity.Test.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using TeamCity.Api;
    using Xunit;

    public class ApiClientTest
    {
        [Fact(Skip = "This is an integration test")]
        public async Task CanGetProjects()
        {
            var settings = new ConnectionSettings
            {
                AuthenticationType = AuthenticationType.GuestAuth,
                Url = "http://teamcity.jetbrains.com",
                Version = "10.0"
            };

            var client = new ApiClient(settings);

            var projects = await client.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(projects);
        }

        [Fact(Skip = "This is an integration test")]
        public async Task CanGetBuildTypes()
        {
            var settings = new ConnectionSettings
            {
                AuthenticationType = AuthenticationType.GuestAuth,
                Url = "http://teamcity.jetbrains.com",
                Version = "10.0"
            };

            var client = new ApiClient(settings);

            var buildTypes = await client.GetBuildTypesAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(buildTypes);
        }
    }
}
