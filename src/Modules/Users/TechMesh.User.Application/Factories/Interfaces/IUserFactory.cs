namespace TechMesh.User.Application.Factories.Interfaces;

public interface ICreateUserFactory
{
    Task<Domain.Entities.User> Get(CreateUserCommand request, CancellationToken cancellationToken);
}