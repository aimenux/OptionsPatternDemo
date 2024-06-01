using Api;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Tests;

public class IntegrationTests
{
    [Theory]
    [InlineData(nameof(Choices.ConfigurationOptionsService))]
    [InlineData(nameof(Choices.ConfigurationOptionsMonitorService))]
    [InlineData(nameof(Choices.ConfigurationOptionsSnapshotService))]
    public async Task Should_Get_Entries(string choice)
    {
        // arrange
        var route = $"api/v1/configuration/entries?choice={choice}";

        var entry1 = GenerateEntry(nameof(Settings.Entry1));
        var entry2 = GenerateEntry(nameof(Settings.Entry2));
        var entries = new[] { entry1, entry2 };

        await using var factory = new IntegrationWebApplicationFactory()
            .WithWebHostBuilder(options =>
            {
                options.ConfigureAppConfiguration((_, cfg) =>
                {
                    cfg.AddInMemoryCollection(entries);
                });
            });
        
        var client = factory.CreateClient();

        // act
        var response = await client.GetStringAsync(route);

        // assert
        response.Should().NotBeNullOrWhiteSpace();
        response.Should().Contain(entry1.Value);
        response.Should().Contain(entry2.Value);
    }

    private static KeyValuePair<string, string?> GenerateEntry(string name)
    {
        var key = $"{nameof(Settings)}:{name}";
        var value = Guid.NewGuid().ToString("N");
        return new KeyValuePair<string, string?>(key, value);
    }
}