namespace StoneChallengeBillingApi.Presentation.ApiResponses;

public class BadRequestResponse : BaseErrorResponse
{
    private const string DEFAULT_TITLE = "Erro ao executar a operação";
    public const int STATUS_CODE = 400;
    public BadRequestResponse(string reason) : base(DEFAULT_TITLE, STATUS_CODE)
    {
    }
}