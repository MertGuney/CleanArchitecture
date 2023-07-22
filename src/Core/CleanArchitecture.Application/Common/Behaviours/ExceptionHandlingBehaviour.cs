namespace CleanArchitecture.Application.Common.Behaviours;

public class ExceptionHandlingBehaviour<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseModel<TResponse>
    where TException : Exception
{
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

        return new ErrorModel(ErrorCode.Service, message, $"Method Name: {methodName}, Inner Exception: {innerException}, Stack Trace: {stackTrace}");
    }
}
