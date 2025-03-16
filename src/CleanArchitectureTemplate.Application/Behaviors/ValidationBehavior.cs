using FluentValidation;
using MediatR;

namespace CleanArchitectureTemplate.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            // nếu có bất cứ ValidationFailure nào thì chỉ lấy cái thằng lỗi đầu tiên đẩy ra thông báo 
            var message = failures.Select(x => x.PropertyName + ": " + x.ErrorMessage).FirstOrDefault();
            throw new ValidationException(message);
        }

        return await next();
    }
}