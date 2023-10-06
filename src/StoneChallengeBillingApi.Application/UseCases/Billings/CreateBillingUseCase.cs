using Microsoft.Extensions.Logging;
using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Application.Mappers;
using StoneChallengeBillingApi.Application.UseCases.Billings.Interfaces;
using StoneChallengeBillingApi.Application.Validators.Billings;
using StoneChallengeBillingApi.Domain.Interfaces.Services;
using StoneChallengeBillingApi.Domain.Models;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Application.UseCases.Billings;

public class CreateBillingUseCase : ICreateBillingUseCase
{
    private readonly ILogger<CreateBillingUseCase> _logger;
    private readonly IBillingService _billingService;

    public CreateBillingUseCase(ILogger<CreateBillingUseCase> logger, 
        IBillingService billingService)
    {
        _logger = logger;
        _billingService = billingService;
    }

    public async Task<IOperation> ExecuteAsync(CreateBillingRequestDTO dto)
    {
        dto.Validate(dto, CreateBillingValidator.Instance);
        
        if (!dto.IsValid)
             return Result.CreateFailure(EErrorType.InvalidData, dto.Notifications);
        
        _logger.LogInformation($"Iniciando cadastro de uma nova cobrança para o cliente: {dto.Cpf}");
        
        var billing = dto.MapToEntity();
        
        var result = await _billingService.CreateAsync(billing);
        
        if (result is FailedOperation<Billing>)
            return Result.CreateFailure(EErrorType.InvalidData, result.Errors);
        
        _logger.LogInformation($"Cobrança efetuada com sucesso para o cliente #{billing.CustomerCpf.Value}#");

        return Result.CreateSuccess();
    }
}