using FluentValidation;
using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Domain.Validators;
using StoneChallengeBillingApi.Domain.ValueObjects;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Constants;

namespace StoneChallengeBillingApi.Application.Validators.Billings;

public class CreateBillingValidator : AbstractValidator<CreateBillingRequestDTO>
{
    public static readonly CreateBillingValidator Instance = new();

    public CreateBillingValidator()
    {
        RuleFor(dto => dto.DueDate)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing);
        
        RuleFor(dto => new Cpf(dto.Cpf))
            .SetValidator(CpfValidator.Instance);

        RuleFor(dto => dto.BillingAmount)
            .NotEmpty()
            .WithMessage(Constants.PropIsMissing)
            .NotNull()
            .WithMessage(Constants.PropIsMissing)
            .Must(x => x > 0)
            .WithMessage(Constants.NegativeBilling);
    }
}