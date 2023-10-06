
using StoneChallengeBillingApi.Domain.ValueObjects;

namespace StoneChallengeBillingApi.Domain.Records;

public record ListBillingsQueryParametersRecord(Cpf? Cpf, DateTime? ReferenceDateInitial, DateTime? ReferenceDateEnd) { }