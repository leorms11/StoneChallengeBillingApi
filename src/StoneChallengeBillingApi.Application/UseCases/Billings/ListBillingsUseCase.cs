using System.Globalization;
using Microsoft.Extensions.Logging;
using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Application.Mappers;
using StoneChallengeBillingApi.Application.UseCases.Billings.Interfaces;
using StoneChallengeBillingApi.Domain.Interfaces.Repositories;
using StoneChallengeBillingApi.Domain.Records;
using StoneChallengeBillingApi.Domain.ValueObjects;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Enums;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Application.UseCases.Billings;

public class ListBillingsUseCase : IListBillingsUseCase
{
    private readonly ILogger<ListBillingsUseCase> _logger;
    private readonly IBillingsRepository _billingsRepository;
    private readonly IEnumerable<string> _availablesMoths = new List<string>()
    {
        "janeiro",
        "fevereiro",
        "março",
        "abril",
        "maio",
        "junho",
        "julho",
        "agosto",
        "setembro",
        "outubro",
        "novembro",
        "dezembro"
    };

    public ListBillingsUseCase(ILogger<ListBillingsUseCase> logger, IBillingsRepository billingsRepository)
    {
        _logger = logger;
        _billingsRepository = billingsRepository;
    }

    public async Task<IOperation<IEnumerable<ListBillingsResponseDTO>>> ExecuteAsync(ListBillingsQueryParametersDTO dto)
    {
        var (hasError, errorMessage) = VerifyIfHasErrorOnParameters(in dto);

        if (hasError)
            return Result.CreateFailure<IEnumerable<ListBillingsResponseDTO>>(EErrorType.InvalidParameters,
                errorMessage);

        var queryParametersRecord = ConvertDtoToRecord(in dto);

        var billings = await _billingsRepository.ListAsync(queryParametersRecord);
        
        return Result.CreateSuccess(billings.MapToDto());
    }

    private (bool hasError, string? messageError) VerifyIfHasErrorOnParameters(in ListBillingsQueryParametersDTO dto)
    {
        if (dto.ReferenceMonth is null && string.IsNullOrEmpty(dto.Cpf))
            return (true, "Você precisa informar ao menos um CPF ou um mês de referência.");
        
        if (!string.IsNullOrEmpty(dto.Cpf) && !new Cpf(dto.Cpf).IsValid)
            return (true, "CPF inválido.");

        if (dto.ReferenceMonth is not null && !_availablesMoths.Contains(dto.ReferenceMonth))
            return (true, "Mês inválido. Opções disponíveis: Janeiro, Fevereiro, ...");

        return (false, null);

    }
    
    private ListBillingsQueryParametersRecord ConvertDtoToRecord(in ListBillingsQueryParametersDTO dto)
    {
        DateTime? formattedDateInitial = null;
        DateTime? formattedDateEnd = null;
        Cpf? cpf = null;
        
        if (!string.IsNullOrEmpty(dto.ReferenceMonth))
        {
            var formattedMonthString = char.ToUpper(dto.ReferenceMonth[0]) + dto.ReferenceMonth.Substring(1);
            
            var dateAux = DateTime.ParseExact(formattedMonthString, "MMMM", new CultureInfo("pt-BR"));

            formattedDateEnd = new DateTime(dateAux.Year, dateAux.Month, DateTime.DaysInMonth(dateAux.Year, dateAux.Month));
            formattedDateInitial = new DateTime(dateAux.Year, dateAux.Month, 1);
        }

        if (!string.IsNullOrEmpty(dto.Cpf))
        {
            cpf = new Cpf(dto.Cpf);
        }

        return new(cpf, formattedDateInitial, formattedDateEnd);
    }
}