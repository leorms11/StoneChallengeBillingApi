using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using StoneChallengeBillingApi.Domain.Enums;
using StoneChallengeBillingApi.Presentation.ApiResponses.Factories;

namespace StoneChallengeBillingApi.Presentation.MiddleWares;

[ExcludeFromCodeCoverage]
public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

    public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(await GetDetailedErrorMessage(context, ex));
            await WriteCustomResponseFromException(context, ex, EApiResponseType.BadRequest);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(await GetDetailedErrorMessage(context, ex));
            await WriteCustomResponseFromException(context, ex, EApiResponseType.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(await GetDetailedErrorMessage(context, ex));
            await WriteCustomResponseFromException(context, ex, EApiResponseType.InternalServerError);
        }
    }

    private async Task<string> GetDetailedErrorMessage(HttpContext context, Exception ex)
    {
        var requestQueryString = GetRequestQueryStringValues(context.Request);
        var completePath = $"{context.Request.Method} {context.Request.Path.Value}";
        var requestBody = await ReadBodyAsync(context);

        return $"Message: {ex.Message}" + Environment.NewLine +
               $"| Path: {completePath}" +
               $"| Query: {requestQueryString}" +
               $"| Body: {requestBody}" +
               $"| StackTrace: {ex.StackTrace}" +
               $"| InnerException: {ex.InnerException}";
    }

    private string GetRequestQueryStringValues(HttpRequest request)
        => request.QueryString.HasValue ? request.QueryString.Value : String.Empty;

    private async Task<string> ReadBodyAsync(HttpContext context)
    {
        using var stream = new StreamReader(context.Request.Body);
        stream.BaseStream.Seek(0, SeekOrigin.Begin);
        return await stream.ReadToEndAsync();
    }

    private async Task WriteCustomResponseFromException(
        HttpContext context,
        Exception ex,
        EApiResponseType responseType)
    {
        var response = ApiResponseFactory.Create(responseType)
            .CreateResponse(ex.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}