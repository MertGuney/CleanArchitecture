namespace CleanArchitecture.Application.Common.Extensions;

public class ExceptionMiddlewareExtensions
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddlewareExtensions> _logger;

    public ExceptionMiddlewareExtensions(RequestDelegate next, ILogger<ExceptionMiddlewareExtensions> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature is not null)
        {
            context.Response.StatusCode = contextFeature.Error switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError,
            };
            var response = ResponseModel<NoContentModel>.Failure(FailureTypes.APPLICATION_EXCEPTION, GetTitle(exception), "Internal Server Error.", context.Response.StatusCode);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    private static string GetTitle(Exception exception)
        => exception switch
        {
            CustomApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };
}
