namespace TechMesh.User.Application.Abstracts.Command;

public abstract class Command<TResult> : IRequest<TResult> where TResult : class
{
    [JsonIgnore] public ValidationResult ValidationResult { get; set; }

    public Command()
        => ValidationResult = new ValidationResult();
}