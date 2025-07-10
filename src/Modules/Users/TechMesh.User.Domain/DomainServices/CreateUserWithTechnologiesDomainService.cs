namespace TechMesh.User.Domain.DomainServices;

public class CreateUserWithTechnologiesDomainService : ICreateUserWithTechnologiesDomainService
{
    private readonly IUserRepository _userRepository;
    private readonly ITechnologyRepository _technologyRepository;

    public CreateUserWithTechnologiesDomainService(
        IUserRepository userRepository,
        ITechnologyRepository technologyRepository)
    {
        _userRepository = userRepository;
        _technologyRepository = technologyRepository;
    }

    public async Task<Entities.User> Execute(
        FullName fullName,
        Email email,
        BirthDate birthDate,
        PhoneNumber phoneNumber,
        Address address,
        EUserLevel level,
        List<string> technologiesNames,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(email.Address, cancellationToken);

        DomainException.When(user is not null, "User already exists.");

        var technologies = await GetOrCreateTechnologiesAsync(technologiesNames, cancellationToken);

        user = Entities.User.Create(fullName, email, birthDate, phoneNumber, address, level);

        var userTechnologies = technologies
            .Select(technology => new UserTechnology(user, technology))
            .ToArray();

        user.AddTechnologies(userTechnologies);

        return user;
    }

    private async Task<List<Technology>> GetOrCreateTechnologiesAsync(
        List<string> technologiesNames,
        CancellationToken cancellationToken)
    {
        var technologiesNamesUpper = technologiesNames
            .Select(name => name.ToUpper())
            .ToList();

        var technologies = await _technologyRepository
            .GetByNamesAsync(technologiesNamesUpper, cancellationToken) ?? [];

        var newTechonoglies = technologiesNamesUpper
            .Where(newTechnologyName =>
                !technologies.Any(tExisting => newTechnologyName == tExisting.Name.Value.ToUpper()))
            .Select(name => Technology.Create(name))
            .ToList();

        if (!newTechonoglies.Any())
            return technologies;

        newTechonoglies = await _technologyRepository.CreateAsync(newTechonoglies, cancellationToken);

        technologies.AddRange(newTechonoglies);

        return technologies;
    }
}