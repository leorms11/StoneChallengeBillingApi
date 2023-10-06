using System.Net;
using FluentAssertions;
using StoneChallengeBillingApi.Test.Integration.Fixtures;

namespace StoneChallengeBillingApi.Test.Integration;

[Collection(nameof(IntegrationTestFixtureCollection))]
public class SwaggerTest
{
    private readonly IntegrationTestFixture _fixture;

    public SwaggerTest(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestSwaggerJson()
    {
        var response = await _fixture.GetSwagger();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}