using FluentValidation;
using StoneChallengeBillingApi.Domain.ValueObjects;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Constants;

namespace StoneChallengeBillingApi.Domain.Validators;

public class CpfValidator : AbstractValidator<Cpf>
{
    public static readonly CpfValidator Instance = new();

    public CpfValidator()
    {
        RuleFor(cpf => cpf)
            .Must(cpf => cpf.IsValid)
            .WithMessage(Constants.InvalidField("CPF"));

        RuleFor(cpf => cpf.Value)
            .NotEmpty()
            .WithMessage(Constants.InvalidField("cpf"))
            .NotNull()
            .WithMessage(Constants.InvalidField("cpf"));
    }
}