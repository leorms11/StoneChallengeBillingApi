using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Domain.ValueObjects;
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
    
    public Cpf GenerateValidCpf()
    {
        var isValid = false;
        var cpf = new Cpf(string.Empty);
        
        while (!isValid)
        {
            cpf = new Cpf(new Faker().Person.Cpf());
            isValid = cpf.IsValid;
        }

        return cpf;
    }
    
    public CreateBillingRequestDTO GenerateValidCreateRequestDto()
        => new()
        {
            DueDate = DateTimeHelpers.GetSouthAmericaDateTimeNow().Date,
            BillingAmount =  new decimal(new Random().Next(1000, 9999)),
            Cpf = GenerateValidCpf().Value.ToString()
        };

    public void Dispose()
    {
        Client.Dispose();
        Factory?.Dispose();
    }
}