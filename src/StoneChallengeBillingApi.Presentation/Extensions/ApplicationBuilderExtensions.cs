using StoneChallengeBillingApi.Presentation.MiddleWares;

namespace StoneChallengeBillingApi.Presentation.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
}