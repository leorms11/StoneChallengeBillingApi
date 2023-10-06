using Microsoft.AspNetCore.Mvc;
using StoneChallengeBillingApi.Application.DTOs.Billings;
using StoneChallengeBillingApi.Application.UseCases.Billings.Interfaces;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace StoneChallengeBillingApi.Presentation.Controllers;

[ApiVersion("1.0")]
[Consumes("application/json")]
[Route("api/v{version:apiVersion}/billings", Name = "Billings")]
public class BillingController : BaseController
{
    private readonly ICreateBillingUseCase _createBillingUseCase;
    private readonly IListBillingsUseCase _listBillingsUseCase;
    
    public BillingController(ICreateBillingUseCase createBillingUseCase, IListBillingsUseCase listBillingsUseCase)
    {
        _createBillingUseCase = createBillingUseCase;
        _listBillingsUseCase = listBillingsUseCase;
    }

    /// <param name="referenceMonth" example="janeiro">O parametro referenceMonth deve ser o nome do mês escrito por extenso e em pt-BR..</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ListBillingsResponseDTO>), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Listagem das cobranças.")]
    public async Task<ActionResult<IEnumerable<ListBillingsResponseDTO>>> ListAsync([FromQuery] ListBillingsQueryParametersDTO dto)
    {
        var result = await _listBillingsUseCase.ExecuteAsync(dto);

        return result is FailedOperation ? BadRequest(result) : Ok(result.Data);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
    [SwaggerOperation(Summary = "Criar uma nova cobrança.")]
    public async Task<ActionResult<IOperation>> CreateAsync([FromBody] CreateBillingRequestDTO dto)
    {
        var result = await _createBillingUseCase.ExecuteAsync(dto);

        return result is FailedOperation ? BadRequest(result) : Ok();
    }
}