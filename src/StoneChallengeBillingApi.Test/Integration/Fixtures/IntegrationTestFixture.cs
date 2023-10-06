using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Helpers;
using StoneChallengeBillingApi.Presentation;
using StoneChallengeBillingApi.Test.Integration.Config;

namespace StoneChallengeBillingApi.Test.Integration.Fixtures;

[CollectionDefinition(nameof(IntegrationTestFixtureCollection))]
public class IntegrationTestFixtureCollection : ICollectionFixture<IntegrationTestFixture> {}

public class IntegrationTestFixture : IDisposable
{
    private readonly BillingAppFactory Factory;
    public readonly HttpClient Client;
    
    public IntegrationTestFixture()
    {
        Factory = new BillingAppFactory();
        Client = Factory.CreateClient();
    }

    public async Task<HttpResponseMessage> GetSwagger()
        => await Client.GetAsync("/swagger/v1/swagger.json");
    
    public CreateBillingRequestDTO GenerateValidCreateRequestDto()
        => new()
        {
            DueDate = DateTimeHelpers.GetSouthAmericaDateTimeNow().Date,
            BillingAmount =  new decimal(new Random().Next(1000, 9999)),
            Cpf = new Faker().Person.Cpf()
        };

    public void Dispose()
    {
        Client.Dispose();
        Factory?.Dispose();
    }
}