using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StoneChallengeBillingApi.Presentation.Configurations.Swagger;

public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters is null)
            return;

        foreach (var parameter in operation.Parameters)
        {
            var description = context.ApiDescription
                .ParameterDescriptions
                .First(apd => apd.Name == parameter.Name);

            var routeInfo = description.RouteInfo;

            operation.Deprecated = OpenApiOperation.DeprecatedDefault;
            parameter.Description ??= description.ModelMetadata.Description;

            if (routeInfo is null)
                continue;

            if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null)
                parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue?.ToString());

            parameter.Required |= !routeInfo.IsOptional;

        }
    }
}