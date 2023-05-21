using CleanArchitecture.Shared;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Common.Extensions
{
    public class ExceptionMiddlewareExtensions
    {
        private const int ServerErrorCode = 1000;
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
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature is not null)
            {
                var response = ResponseModel<NoContentModel>.Failure(ServerErrorCode, "Internal Server Error.", "", context.Response.StatusCode);

                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
