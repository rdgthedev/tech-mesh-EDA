namespace TechMesh.User.Application.Factories.Interfaces;

public interface IUserFactory
{
    Domain.Entities.User Create(CreateUserCommand request);
}