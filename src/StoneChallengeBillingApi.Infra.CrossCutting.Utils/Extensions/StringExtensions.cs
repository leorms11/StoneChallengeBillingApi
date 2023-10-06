namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Extensions;

public static class StringExtensions
{
    public static string OnlyNumbers(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        Span<char> result = stackalloc char[value.Length];
        var index = 0;

        foreach (char c in value)
        {
            if (IsNumber(c))
                result[index++] = c;
        }

        return result[..index].ToString();
    }

    private static bool IsNumber(char c)
        => c is >= '0' and <= '9';
}