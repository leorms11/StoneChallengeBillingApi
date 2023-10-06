using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoneChallengeBillingApi.Infra.CrossCutting.IoC;
using StoneChallengeBillingApi.Infra.Persistence.Context;
using StoneChallengeBillingApi.Presentation.Configurations.Swagger;
using StoneChallengeBillingApi.Presentation.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StoneChallengeBillingApi.Presentation;

public interface IStartup
{
    IConfiguration Configuration { get; }
    void ConfigureServices(IServiceCollection serviceCollection);
    void Configure(WebApplication webApplication, IConfiguration configuration, IWebHostEnvironment hostEnvironment);
}

[ExcludeFromCodeCoverage]
public class Startup : IStartup
{
    public IConfiguration Configuration { get; }
    private readonly bool IsDevelopment;

    public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        Configuration = configuration;
        IsDevelopment = hostEnvironment.IsDevelopment();
    }

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        // if (IsDevelopment)
        //     EnvFile.LoadEnvVariables();
        
        serviceCollection.AddDbContext<PostgreSqlDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        serviceCollection.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        });

        serviceCollection.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        serviceCollection.AddSwaggerConfiguration();
        serviceCollection.RegisterServices();

        serviceCollection.AddEndpointsApiExplorer();
    }

    public void Configure(WebApplication webApplication, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsProduction())
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/swagger.json", "Stone Challenger API");
                options.RoutePrefix = string.Empty;
            });
        }

        webApplication.UseGlobalErrorHandling();

        webApplication.UseRouting();
        webApplication.UseAuthorization();

        webApplication.MapControllers();
    }
}