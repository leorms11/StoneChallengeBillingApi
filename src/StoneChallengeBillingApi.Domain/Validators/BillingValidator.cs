using FluentValidation;
using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Constants;

namespace StoneChallengeBillingApi.Domain.Validators;

public class BillingValidator : AbstractValidator<Billing>
{
    public static readonly BillingValidator Instance = new();

    public BillingValidator()
    {
        RuleFor(billing => billing.DueDate)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing);
        
        RuleFor(billing => billing.CustomerCpf)
            .NotNull()
            .WithMessage(Constants.PropIsMissing)
            .Must(x => x.IsValid)
            .WithMessage(Constants.InvalidField("cpf"));

        RuleFor(billing => billing.BillingAmount)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing)
            .Must(x => x > 0)
            .WithMessage(Constants.NegativeBilling);
    }
}