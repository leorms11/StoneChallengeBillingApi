using FluentValidation;
using FluentValidation.Results;

namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;

public abstract class Notifiable
{
    public bool IsValid { get; private set; }
    public IEnumerable<ResultErrorField> Notifications { get; private set; } = new List<ResultErrorField>();

    public void Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        var validationResult = validator.Validate(model);
        IsValid = validationResult.IsValid;

        if (IsValid)
            return;

        Notifications = GetFormatedErros(validationResult.Errors);
    }

    private IEnumerable<ResultErrorField> GetFormatedErros(List<ValidationFailure> failures)
    {
        foreach (var failure in failures)
            yield return new ResultErrorField(failure.PropertyName, failure.ErrorMessage);
    }
}