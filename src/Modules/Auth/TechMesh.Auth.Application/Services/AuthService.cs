namespace TechMesh.Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IRoleService _roleService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IPasswordHasherAdapter _passwordHasherAdapter;
    private readonly IUserServiceApiRefitAdapter _userServiceApiRefitAdapter;

    public AuthService(
        IUserService userService,
        IJwtService jwtService,
        IRoleService roleService,
        IUnitOfWork unitOfWork,
        IPasswordHasherAdapter passwordHasherAdapter,
        IUserServiceApiRefitAdapter userServiceApiRefitAdapter)
    {
        _userService = userService;
        _jwtService = jwtService;
        _roleService = roleService;
        _unitOfWork = unitOfWork;
        _passwordHasherAdapter = passwordHasherAdapter;
        _userServiceApiRefitAdapter = userServiceApiRefitAdapter;
    }

    public async Task<Result<AuthTokensResponse>> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var userExistsResponse = await _userService
            .ExistsAsync(u => u.Email.ToUpper() == request.Email.ToUpper(), cancellationToken);

        if (userExistsResponse.IsSuccess)
            return Result<AuthTokensResponse>
                .Failure(Convert.ToInt32(HttpStatusCode.BadRequest), "User already exists!");

        var passwordHash = _passwordHasherAdapter.Hash(request.Password);

        var roleResponse = await _roleService.GetByIdOrDefault(request.RoleId, cancellationToken);

        if (!roleResponse.IsSuccess)
            return Result<AuthTokensResponse>
                .Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Role not found!");

        var user = User.Create(request.Email, passwordHash, roleResponse.Data!.Id);

        var apiResponse = await _userServiceApiRefitAdapter.CreateUserAsync(Mapper.Map(request));

        if (!apiResponse.IsSuccess)
            return Result<AuthTokensResponse>
                .Failure(apiResponse.StatusCode, apiResponse.Message, apiResponse.Errors.ToArray());

        await _userService.CreateAsync(user, cancellationToken);

        var accessToken = await _jwtService.GenerateAccessToken(user.Id, roleResponse.Data.Name);

        var refreshToken = (await _jwtService.GenerateRefreshToken(user.Id, cancellationToken)).Data!.Value;

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<AuthTokensResponse>
            .Success(
                Convert.ToInt32(HttpStatusCode.Created),
                new AuthTokensResponse(accessToken, refreshToken),
                "User registered with success!");
    }

    public async Task<Result<AuthTokensResponse>> SignInAsync(
        SignInUserRequest signInUserRequest,
        CancellationToken cancellationToken)
    {
        var userResponse = await _userService
            .GetUserWithRoleByEmailAsync(signInUserRequest.Email, cancellationToken);

        if (!userResponse.IsSuccess)
            return Result<AuthTokensResponse>
                .Failure(Convert.ToInt32(HttpStatusCode.BadRequest), "Email or Password invalid!");

        var userData = userResponse.Data;

        var isPasswordValid = _passwordHasherAdapter.Verify(signInUserRequest.Password, userData?.PasswordHash!);

        if (!isPasswordValid)
            return Result<AuthTokensResponse>
                .Failure(Convert.ToInt32(HttpStatusCode.BadRequest), "Email or Password invalid!");

        var accessToken = await _jwtService.GenerateAccessToken(userData!.Id, userData.Role!.Name);

        var refreshToken = (await _jwtService.GenerateRefreshToken(userData.Id, cancellationToken)).Data;

        return Result<AuthTokensResponse>
            .Success(
                Convert.ToInt32(HttpStatusCode.OK),
                new AuthTokensResponse(accessToken, refreshToken!.Value),
                "User successfully signed in!");
    }
}