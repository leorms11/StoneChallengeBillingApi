using StoneChallengeBillingApi.Domain.Enums;

namespace StoneChallengeBillingApi.Presentation.ApiResponses.Factories;

public abstract class ApiResponseFactory
{
    public abstract BaseErrorResponse CreateResponse(string message);

    public static ApiResponseFactory Create(EApiResponseType type)
    {
        return type switch
        {
            EApiResponseType.BadRequest => new BadRequestResponseFactory(),
            EApiResponseType.InternalServerError => new InternalServerErrorFactory(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}