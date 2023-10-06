using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Extensions;

namespace StoneChallengeBillingApi.Domain.ValueObjects;

public class Cpf : ValueObject
{
    private const int CPF_LENGTH = 11;
    private readonly int[] _multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    private readonly int[] _multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    private readonly IEnumerable<string> _wrongCpfList = new List<string>()
    {
        "",
        "000000000000",
        "111111111111",
        "222222222222",
        "33333333333",
        "444444444444",
        "555555555555",
        "666666666666",
        "777777777777",
        "888888888888",
        "999999999999",
    };

    public readonly long Value;
    public readonly bool IsValid;

    protected Cpf() { }

    public Cpf(string? value)
    {
        value ??= string.Empty;

        long.TryParse(value.Trim().OnlyNumbers(), out Value);
        IsValid = VerifyIfIsValid(Value);
    }

    private bool VerifyIfIsValid(long cpfNumber)
    {
        var value = cpfNumber.ToString();
        if (value.Length is not CPF_LENGTH || _wrongCpfList.Contains(value))
            return false;

        for (var i = 0; i < 10; i++)
            if (i.ToString().PadLeft(11, char.Parse(i.ToString())) == value)
                return false;

        var tempCpf = value[..9];
        var sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * _multiplier1[i];

        var rest = sum % 11;
        rest = rest < 2 ? 0 : 11 - rest;

        var digit = rest.ToString();
        tempCpf += digit;
        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * _multiplier2[i];

        rest = sum % 11;
        rest = rest < 2 ? 0 : 11 - rest;

        digit += rest.ToString();

        return value.EndsWith(digit);
    }

    public static implicit operator Cpf(string value)
        => new(value);
}