using System.Diagnostics.CodeAnalysis;

namespace StoneChallengeBillingApi.Presentation.Extensions;

[ExcludeFromCodeCoverage]
public static class WebApplicationBuilderExtensions
{
    public static WebApplication UseStartup<TStartup>(this WebApplicationBuilder builder, bool isTestContext)
    {
        var startup = Activator.CreateInstance(typeof(TStartup), builder.Configuration, builder.Environment) as IStartup
            ?? throw new ArgumentException("Erro ao tentar instanciar a classe Startup.cs.");

        startup.ConfigureServices(builder.Services);

        builder.Services.AddApiConfiguration();
        builder.Services.AddSwaggerGen();

        builder.WebHost.UseKestrel(options =>
        {
            options.ListenAnyIP(5134);
            options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
        });

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();

        startup.Configure(app, app.Configuration, app.Environment);
        return app;
    }
}