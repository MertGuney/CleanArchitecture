﻿using CleanArchitecture.Shared;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CleanArchitecture.Application.Common.Behaviours
{
    public class ExceptionHandlingBehaviour<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException> where TRequest : notnull where TResponse : class where TException : Exception
    {
        private const int ErrorCode = 4001;
        private readonly ILogger<ExceptionHandlingBehaviour<TRequest, TResponse, TException>> _logger;

        public ExceptionHandlingBehaviour(ILogger<ExceptionHandlingBehaviour<TRequest, TResponse, TException>> logger)
        {
            _logger = logger;
        }

        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            var response = CreateExceptionError(exception);

            _logger.LogError(JsonSerializer.Serialize(response));

            state.SetHandled(response as TResponse);

            return Task.FromResult(response);
        }

        private static ErrorModel CreateExceptionError(TException exception)
        {
            var methodName = exception.TargetSite?.DeclaringType?.FullName;
            var message = exception.Message;
            var innerException = exception.InnerException?.Message;
            var stackTrace = exception.StackTrace;

            return new ErrorModel(ErrorCode, message, $"Method Name: {methodName}, Inner Exception: {innerException}, Stack Trace: {stackTrace}");
        }
    }
}