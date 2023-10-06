using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StoneChallengeBillingApi.Presentation.Configurations.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            options.EnableAnnotations();
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        => new()
        {
            Title = "Stone Challenger API",
            Version = description.ApiVersion.ToString(),
            Description = "API Rest para Processamento de Cobrança.",
            Contact = new()
            {
                Name = "Leonardo Ruiz",
                Email = "leonardo.fakeemail@gmail.com"
            }
        };
}