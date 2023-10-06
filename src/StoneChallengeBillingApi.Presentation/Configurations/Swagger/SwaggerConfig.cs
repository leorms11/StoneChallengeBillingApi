namespace StoneChallengeBillingApi.Presentation.Configurations.Swagger;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection serviceCollection)
    {
        if (serviceCollection is null)
            throw new InvalidOperationException(nameof(serviceCollection));

        serviceCollection.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.OperationFilter<SwaggerDefaultValues>();
        });
    }
}