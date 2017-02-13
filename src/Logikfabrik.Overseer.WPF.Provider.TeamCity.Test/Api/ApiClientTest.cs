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
        //[Fact]
        public async Task CanGetProjects()
        {
            var baseUri = BaseUriHelper.GetBaseUri("http://teamcity.jetbrains.com", "10.0", AuthenticationType.GuestAuth);

            var client = new ApiClient(baseUri);

            var projects = await client.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(projects);
        }

        //[Fact]
        public async Task CanGetBuildTypes()
        {
            var baseUri = BaseUriHelper.GetBaseUri("http://teamcity.jetbrains.com", "10.0", AuthenticationType.GuestAuth);

            var client = new ApiClient(baseUri);

            var projects = await client.GetProjectsAsync(CancellationToken.None).ConfigureAwait(false);

            foreach (var project in projects.Project)
            {
                var buildTypes = await client.GetBuildTypesAsync(project.Id, CancellationToken.None).ConfigureAwait(false);

                Assert.NotNull(buildTypes);
            }
        }
    }
}
