using CleanArchitecture.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, ResponseModel<TResponse>> where TRequest : class
    {
        private const int ValidationErrorCode = 4000;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<ResponseModel<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ResponseModel<TResponse>> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var results = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            var failures = results.SelectMany(x => x.Errors).Where(x => x is not null).ToList();

            if (!failures.Any())
            {
                return await next.Invoke();
            }

            var errors = failures.Select(f => new ErrorModel(ValidationErrorCode, f.PropertyName, f.ErrorMessage)).ToList();

            var response = await ResponseModel<TRequest>.FailureAsync(errors, StatusCodes.Status422UnprocessableEntity);

            return await Task.FromResult(response as ResponseModel<TResponse>);
        }
    }
}
