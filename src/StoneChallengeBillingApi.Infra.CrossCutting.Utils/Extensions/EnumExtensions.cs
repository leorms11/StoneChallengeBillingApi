using System.ComponentModel;

namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T source)
    {
        if (source is null)
            return string.Empty;

        var fieldInfo = source.GetType().GetField(source.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : source.ToString();
    }
}