using Bogus;
using Bogus.Extensions.Brazil;
using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Domain.ValueObjects;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Helpers;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Test.Unit.Fixtures;

[CollectionDefinition(nameof(BillingCollection))]
public class BillingCollection : ICollectionFixture<BillingTestFixture> {}

public class BillingTestFixture : IDisposable
{
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

    public Billing GenerateValidBilling()
        => Billing.Create(
            DateTimeHelpers.GetSouthAmericaDateTimeNow().Date,
            new decimal(new Random().Next(1000, 9999)),
            GenerateValidCpf().Value.ToString());

    public IEnumerable<Billing> GenerateValidBillingList(int numberToGenerate)
    {
        for (int i = 1; i <= numberToGenerate; i++)
            yield return Billing.Create(
                DateTimeHelpers.GetSouthAmericaDateTimeNow().Date,
                new decimal(new Random().Next(1000, 9999)),
                GenerateValidCpf().Value.ToString());
    }
         
    
    public Billing GenerateInvalidBilling()
        => Billing.Create(
            DateTimeHelpers.GetSouthAmericaDateTimeNow().Date,
            new decimal(new Random().Next(1000, 9999)),
            "11111111111");

    public CreateBillingRequestDTO GenerateValidCreateRequestDto()
        => new()
        {
            DueDate = DateTimeHelpers.GetSouthAmericaDateTimeNow().Date,
            BillingAmount =  new decimal(new Random().Next(1000, 9999)),
            Cpf = GenerateValidCpf().Value.ToString()
        };
    
    public CreateBillingRequestDTO GenerateInvalidCreateRequestDto()
        => new()
        {
            DueDate = DateTimeHelpers.GetSouthAmericaDateTimeNow().Date,
            BillingAmount =  new decimal(new Random().Next(1000, 9999)),
            Cpf = "wrong_cpf"
        };

    public ListBillingsQueryParametersDTO GenerateListQueryParametersDto(string? month = null, string? cpf = null)
        => new()
        {
            ReferenceMonth = month,
            Cpf = cpf
        };
    
    public IOperation<Billing> GenerateSuccessResultFromBilling()
        => Result.CreateSuccess(GenerateValidBilling());
    
    public IOperation<Billing> GenerateFailedResultFromBilling(IEnumerable<ResultErrorField> notifications)
        => Result.CreateFailure<Billing>(EErrorType.InvalidData, notifications);

    public void Dispose()
    {
    }
}