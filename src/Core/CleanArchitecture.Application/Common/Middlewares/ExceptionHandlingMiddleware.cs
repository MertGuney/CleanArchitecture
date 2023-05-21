using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Shared;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CleanArchitecture.Application.Common.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private const int ValidationErrorCode = 100;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            List<ErrorModel> errors = new();
            foreach (var error in GetErrors(exception))
            {
                errors.Add(new ErrorModel(ValidationErrorCode, GetTitle(exception), error.ErrorMessage));
            }

            var response = await ResponseModel<NoContentModel>.FailureAsync(errors, statusCode);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception)
            => exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(Exception exception)
            => exception switch
            {
                Exceptions.ApplicationException applicationException => applicationException.Title,
                _ => "Server Error"
            };

        private static IEnumerable<ValidationFailure> GetErrors(Exception exception)
        {
            IEnumerable<ValidationFailure> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors;
            }
            return errors;
        }
    }
}
