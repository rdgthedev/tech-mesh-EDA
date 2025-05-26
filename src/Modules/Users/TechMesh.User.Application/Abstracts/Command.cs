namespace TechMesh.User.Application.Abstracts;

public abstract class Command<TResult> : IRequest<TResult> where TResult : class
{
    [JsonIgnore] public ValidationResult ValidationResult { get; protected set; }

    protected Command()
        => ValidationResult = new ValidationResult();

    protected abstract ValidationResult Validate();
}