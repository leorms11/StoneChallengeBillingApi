using Microsoft.AspNetCore.Mvc;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications.Interfaces;

namespace StoneChallengeBillingApi.Presentation.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(typeof(IOperation), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(IOperation), StatusCodes.Status500InternalServerError)]
public abstract class BaseController : ControllerBase
{

}