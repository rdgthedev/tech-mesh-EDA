namespace TechMesh.User.Application.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICreateUserFactory _createUserFactory;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ICreateUserFactory createUserFactory)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _createUserFactory = createUserFactory;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _createUserFactory.Get(request, cancellationToken);

        await _userRepository.CreateAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<string>.Success(Convert.ToInt32(HttpStatusCode.Created), user.Id.ToString());
    }
}