namespace TechMesh.Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IRoleService _roleService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IPasswordHasherAdapter _passwordHasherAdapter;

    public AuthService(
        IUserService userService,
        IJwtService jwtService,
        IRoleService roleService,
        IUnitOfWork unitOfWork,
        IPasswordHasherAdapter passwordHasherAdapter)
    {
        _userService = userService;
        _jwtService = jwtService;
        _roleService = roleService;
        _unitOfWork = unitOfWork;
        _passwordHasherAdapter = passwordHasherAdapter;
    }

    public async Task<Result<AuthTokensResponse>> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var userExistsResponse = await _userService
            .ExistsAsync(u => u.Email == request.Email, cancellationToken);
        
        if (userExistsResponse.IsSuccess)
            return Result<AuthTokensResponse>.Failure(400, "User already exists!");

        var passwordHash = _passwordHasherAdapter.Hash(request.Password);

        var roleResponse = await _roleService.GetByIdOrDefault(request.RoleId, cancellationToken);

        if (!roleResponse.IsSuccess)
            return Result<AuthTokensResponse>.Failure(404, "Role not found!");

        var user = User.Create(request.Email, passwordHash, roleResponse.Data!.Id);

        // chamar endpoint / api / users(POST) -> User Service 

        var accessToken = await _jwtService.GenerateAccessToken(user.Id, roleResponse.Data!.Name);

        var refreshToken = (await _jwtService.GenerateRefreshToken(user.Id, cancellationToken)).Data?.Value;

        await _userService.CreateAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<AuthTokensResponse>
            .Success(201, new AuthTokensResponse(accessToken, refreshToken!), "User registered with success!");
    }

    public async Task<Result<AuthTokensResponse>> SignInAsync(
        SignInUserRequest signInUserRequest,
        CancellationToken cancellationToken)
    {
        var userResponse = await _userService
            .GetUserWithRoleByEmailAsync(signInUserRequest.Email, cancellationToken);

        if (!userResponse.IsSuccess)
            return Result<AuthTokensResponse>.Failure(400, "Email or Password invalid!");

        var userData = userResponse.Data;

        var isPasswordValid = _passwordHasherAdapter.Verify(signInUserRequest.Password, userData?.PasswordHash!);

        if (!isPasswordValid)
            return Result<AuthTokensResponse>.Failure(400, "Email or Password invalid!");

        var accessToken = await _jwtService.GenerateAccessToken(userData!.Id, userData.Role!.Name);

        var refreshToken = (await _jwtService.GenerateRefreshToken(userData.Id, cancellationToken)).Data;

        return Result<AuthTokensResponse>
            .Success(new AuthTokensResponse(accessToken, refreshToken!.Value), "User successfully signed in!");
    }
}