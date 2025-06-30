namespace TechMesh.User.Domain.Interfaces;

public interface ICreateUserWithTechnologiesDomainService
{
    Task<Entities.User> Execute(
        FullName fullName,
        Email email,
        BirthDate birthDate,
        PhoneNumber phoneNumber,
        Address address,
        EUserLevel level,
        List<string> technologies,
        CancellationToken cancellationToken);
}