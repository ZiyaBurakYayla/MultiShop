using FluentValidation;
using MediatR;

namespace MultiShop.Order.Application.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct)));
                var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Any()) throw new ValidationException(failures);
            }
            return await next();
        }
    }

}
