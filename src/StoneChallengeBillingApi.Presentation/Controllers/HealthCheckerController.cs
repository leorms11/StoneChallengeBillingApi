using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StoneChallengeBillingApi.Presentation.Controllers;

[Consumes("application/json")]
[Route("api/health-checker", Name = "Health Checker")]
public class HealthCheckerController : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Verifica a saúde do servidor")]
    public IActionResult CheckAsync()
        => Ok("The server is running!");
}