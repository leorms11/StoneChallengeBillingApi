using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using StoneChallengeBillingApi.Application.UseCases.Billings.Interfaces;
using StoneChallengeBillingApi.Infra.CrossCutting.IoC;
using StoneChallengeBillingApi.Test.Integration.Config;
using StoneChallengeBillingApi.Test.Integration.Fixtures;

namespace StoneChallengeBillingApi.Test.Integration;

[Collection(nameof(IntegrationTestFixtureCollection))]
public class BillingControllerTest
{
    private readonly IntegrationTestFixture _fixture;

    private readonly ICreateBillingUseCase _createBillingUseCase;
    private readonly IListBillingsUseCase _listBillingsUseCase;

    public BillingControllerTest(
        IntegrationTestFixture fixture)
    {
        _fixture = fixture;
        _createBillingUseCase = BootStrapper.GetInstance<ICreateBillingUseCase>();
        _listBillingsUseCase = BootStrapper.GetInstance<IListBillingsUseCase>();
    }

    [Fact]
    public async Task ItShouldBeAbleToCreateABilling()
    {
        // Arrange 
        var arr = _fixture.GenerateValidCreateRequestDto();
        var response = await _fixture.Client.PostAsJsonAsync("api/v1/billings", arr);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task ItShouldBeAbleToListBillings()
    {
        // Arrange 
        var response = await _fixture.Client.GetAsync("api/v1/billings?referenceMonth=outubro");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}