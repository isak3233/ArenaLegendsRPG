
using ArenaLegendsRPG.API;

namespace ArenaLegendsRPG.IntegrationTests.ApiTests;

public class ApiHealthyTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiHealthyTest(CustomWebApplicationFactory<Program> factory)
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