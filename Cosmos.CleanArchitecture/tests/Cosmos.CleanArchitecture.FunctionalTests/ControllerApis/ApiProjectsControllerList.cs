using Ardalis.HttpClientTestExtensions;
using Cosmos.CleanArchitecture.Web;
using Cosmos.CleanArchitecture.Web.ApiModels;
using Xunit;

namespace Cosmos.CleanArchitecture.FunctionalTests.ControllerApis;

[Collection("Sequential")]
public class ProjectCreate : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public ProjectCreate(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsOneProject()
  {
    var result = await _client.GetAndDeserializeAsync<IEnumerable<ProjectDTO>>("/api/projects");

    Assert.Single(result);
    Assert.Contains(result, i => i.Name == SeedData.TestProject1.Name);
  }
}
