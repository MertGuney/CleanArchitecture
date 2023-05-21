using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArchitecture.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators.Select(x => x.Validate(context)).SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(x => x.PropertyName, x => x.ErrorMessage, (propertyName, errorMessage) => new ValidationFailure()
                {
                    PropertyName = propertyName,
                    ErrorMessage = errorMessage.Distinct().FirstOrDefault()
                }).AsEnumerable();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
            return await next();
        }
    }
}
