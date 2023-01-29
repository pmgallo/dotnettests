using Ardalis.HttpClientTestExtensions;
using Cosmos.CleanArchitecture.Web;
using Cosmos.CleanArchitecture.Web.Endpoints.ContributorEndpoints;
using Xunit;

namespace Cosmos.CleanArchitecture.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class ContributorList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public ContributorList(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsTwoContributors()
  {
    var result = await _client.GetAndDeserializeAsync<ContributorListResponse>("/Contributors");

    Assert.Equal(2, result.Contributors.Count);
    Assert.Contains(result.Contributors, i => i.Name == SeedData.Contributor1.Name);
    Assert.Contains(result.Contributors, i => i.Name == SeedData.Contributor2.Name);
  }
}
