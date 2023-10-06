namespace StoneChallengeBillingApi.Infra.CrossCutting.Utils.Helpers;

public class DateTimeHelpers
{
    public const string TIME_ZONE_WIN = "E. South America Standard Time";

    public static DateTime GetSouthAmericaDateTimeNow()
    {
        var currentDate = DateTime.UtcNow;
        var timeZoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x
            => x.Id == TIME_ZONE_WIN);

        return TimeZoneInfo.ConvertTimeFromUtc(currentDate, timeZoneInfo);
    }
}