using Microsoft.AspNetCore.Mvc;

namespace StoneChallengeBillingApi.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConfiguration>(serviceProvider =>
        {
            var env = serviceProvider.GetService<IWebHostEnvironment>();
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();

            return builder.Build();
        });

        serviceCollection.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        serviceCollection.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return serviceCollection;
    }
}