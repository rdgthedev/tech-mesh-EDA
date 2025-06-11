namespace TechMesh.User.Application.Factories;

public class UserFactory : IUserFactory
{
    public Domain.Entities.User Create(CreateUserCommand request)
    {
        var address = new Address(
            request.Street,
            request.Neighborhood,
            request.City,
            request.Country,
            request.Number,
            request.State,
            request.ZipCode,
            request.Complement!);

        var user = Domain.Entities.User.Create(
            request.FullName,
            request.Email,
            request.BirthDate,
            request.PhoneNumber,
            address,
            request.Level);

        return user;
    }
}