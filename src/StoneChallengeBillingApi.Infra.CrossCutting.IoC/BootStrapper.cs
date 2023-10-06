using Microsoft.Extensions.DependencyInjection;
using StoneChallengeBillingApi.Application.UseCases.Billings;
using StoneChallengeBillingApi.Application.UseCases.Billings.Interfaces;
using StoneChallengeBillingApi.Domain.Interfaces.Repositories;
using StoneChallengeBillingApi.Domain.Interfaces.Services;
using StoneChallengeBillingApi.Domain.Services;
using StoneChallengeBillingApi.Infra.Persistence.Context;
using StoneChallengeBillingApi.Infra.Persistence.Repositories;

namespace StoneChallengeBillingApi.Infra.CrossCutting.IoC;

public static class BootStrapper
{
    public static IServiceProvider ServiceProvider = null;
    
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .RegisterRepositories()
            .RegisterServicesAndUseCases();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBillingsRepository, BillingsRepository>();
        return serviceCollection;
    }

    private static IServiceCollection RegisterServicesAndUseCases(this IServiceCollection serviceCollection)
    {
        #region UseCases

        serviceCollection.AddScoped<ICreateBillingUseCase, CreateBillingUseCase>();
        serviceCollection.AddScoped<IListBillingsUseCase, ListBillingsUseCase>();

        #endregion
        
        #region Domain Services
        
        serviceCollection.AddScoped<IBillingService, BillingService>();

        #endregion

        return serviceCollection;
    }
    
    public static T GetInstance<T>()
    {
        if (ServiceProvider is null)
            throw new InvalidOperationException("Os services não foram registrados.");

        return (T)ServiceProvider.GetService(typeof(T));
    }
}