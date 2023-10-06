using StoneChallengeBillingApi.Domain.Validators;
using StoneChallengeBillingApi.Domain.ValueObjects;

namespace StoneChallengeBillingApi.Domain.Models;

public class Billing : BaseModel
{
    protected Billing() { }

    private Billing(DateTime dueDate, decimal billingAmount, Cpf cpf)
    {
        DueDate = dueDate;
        BillingAmount = billingAmount;
        CustomerCpf = cpf;
        
        Validate(this, BillingValidator.Instance);
    }

    public DateTime DueDate { get; private set; }
    public decimal BillingAmount { get; private set; }
    public Cpf CustomerCpf { get; private set; }

    public static Billing Create(DateTime dueDate, decimal billingAmount, Cpf cpf)
        => new(dueDate, billingAmount, cpf);
}