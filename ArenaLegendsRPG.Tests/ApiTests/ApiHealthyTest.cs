using ArenaLegendsRPG.Tests.Fixtures;

namespace ArenaLegendsRPG.Tests.ApiTests;

public class ApiHealthyTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ApiHealthyTest(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task Get_HealthyStatus_ReturnsTwoHundred()
    {
        var response = await _client.GetAsync("/healthy");

        response.EnsureSuccessStatusCode();
    }
}