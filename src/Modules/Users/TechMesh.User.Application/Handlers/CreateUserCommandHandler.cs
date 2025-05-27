namespace TechMesh.User.Application.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserFactory _userFactory;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IUserFactory userFactory)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _userFactory = userFactory;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.ValidationResult.IsValid)
            return Result<string>
                .Failure(400, request.ValidationResult.Errors.Select(x => x.ErrorMessage).ToArray());

        var user = _userFactory.Create(request);

        await _userRepository.CreateAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<string>.Success(201, user.Id.ToString());
    }
}