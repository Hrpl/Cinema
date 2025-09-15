using FluentValidation;
using MediatR;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.API.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"Validating request: {typeof(TRequest).Name}");
        Console.WriteLine($"Found validators: {_validators.Count()}");
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(e => e != null)
            .ToList();

        // ReSharper disable once InvertIf
        if (failures.Count > 0)
        {
            var errorMessage = string.Join("; ", failures.Select(e => e.ErrorMessage));

            // Пытаемся достать тип T из CustomResult<T>
            var responseType = typeof(TResponse);
            if (!responseType.IsGenericType || responseType.GetGenericTypeDefinition() != typeof(CustomResult<>))
                throw new InvalidOperationException("TResponse must be of type CustomResult<T>");
            
            var innerType = responseType.GetGenericArguments()[0];
            var failureMethod = typeof(CustomResult<>)
                .MakeGenericType(innerType)
                .GetMethod(nameof(CustomResult<object>.Failure))!;

            var failureResult = failureMethod.Invoke(null, [errorMessage]);
            return (TResponse)failureResult!;

        }

        return await next();
    }
}