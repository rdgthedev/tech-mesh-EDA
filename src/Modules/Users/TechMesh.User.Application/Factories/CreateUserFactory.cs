namespace TechMesh.User.Application.Factories;

public class CreateUserFactory : ICreateUserFactory
{
    private readonly ICreateUserWithTechnologiesDomainService _createUserWithTechnologiesDomainService;

    public CreateUserFactory(ICreateUserWithTechnologiesDomainService createUserWithTechnologiesDomainService)
        => _createUserWithTechnologiesDomainService = createUserWithTechnologiesDomainService;

    public async Task<Domain.Entities.User> Get(CreateUserCommand request, CancellationToken cancellationToken)
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

        var user = await _createUserWithTechnologiesDomainService.Execute(
            request.FullName,
            request.Email,
            request.BirthDate,
            request.PhoneNumber,
            address,
            request.Level,
            request.Technologies,
            cancellationToken);

        return user;
    }
}