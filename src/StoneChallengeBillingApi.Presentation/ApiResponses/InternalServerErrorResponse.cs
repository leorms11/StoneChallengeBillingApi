namespace StoneChallengeBillingApi.Presentation.ApiResponses;

public class InternalServerErrorResponse : BaseErrorResponse
{
    private const string DEFAULT_TITLE = "Erro interno do servidor";
    public const int STATUS_CODE = 500;
    public InternalServerErrorResponse(string reason) : base(DEFAULT_TITLE, STATUS_CODE)
    {
    }
}