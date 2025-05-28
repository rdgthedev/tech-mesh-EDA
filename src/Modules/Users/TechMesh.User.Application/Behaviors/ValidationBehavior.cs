namespace TechMesh.User.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : Command<TResponse>
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is Command<TResponse> command)
        {
            foreach (var validator in _validators)
                command.ValidationResult = await validator.ValidateAsync(request, cancellationToken);

            if (command.ValidationResult.IsValid)
                return await next(cancellationToken);

            var errors = command
                .ValidationResult
                .Errors
                .Select(e => e.ErrorMessage);

            return (TResponse)(object)Result<string>.Failure((int)HttpStatusCode.BadRequest, errors.ToArray());
        }

        return await next(cancellationToken);
    }
}