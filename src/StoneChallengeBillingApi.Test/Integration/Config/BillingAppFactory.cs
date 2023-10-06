using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using StoneChallengeBillingApi.Presentation;

[assembly: InternalsVisibleTo("StoneChallenge.BillingApi.Test.Fixtures")]
namespace StoneChallengeBillingApi.Test.Integration.Config;

internal class BillingAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        
        builder.UseContentRoot(Environment.CurrentDirectory);
        builder.UseEnvironment("Testing");
    }
}