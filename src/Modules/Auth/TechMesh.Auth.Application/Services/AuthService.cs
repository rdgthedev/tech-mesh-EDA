using Microsoft.Extensions.Logging;

namespace TechMesh.Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IRoleService _roleService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IPasswordHasherAdapter _passwordHasherAdapter;
    private readonly IUserServiceApiRefitAdapter _userServiceApiRefitAdapter;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserService userService,
        IJwtService jwtService,
        IRoleService roleService,
        IUnitOfWork unitOfWork,
        IPasswordHasherAdapter passwordHasherAdapter,
        IUserServiceApiRefitAdapter userServiceApiRefitAdapter,
        ITokenService tokenService,
        ILogger<AuthService> logger)
    {
        _userService = userService;
        _jwtService = jwtService;
        _roleService = roleService;
        _unitOfWork = unitOfWork;
        _passwordHasherAdapter = passwordHasherAdapter;
        _userServiceApiRefitAdapter = userServiceApiRefitAdapter;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<Result<AuthTokensResponse>> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting register of the user with email: {email}!", request.Email);

            var userExistsResponse = await _userService
                .ExistsAsync(u => u.Email.ToUpper() == request.Email.ToUpper(), cancellationToken);

            if (userExistsResponse.IsSuccess)
            {
                _logger.LogWarning("User with email {email} already exists in database!", request.Email);

                return Result<AuthTokensResponse>
                    .Failure(Convert.ToInt32(HttpStatusCode.BadRequest), "User already exists!");
            }

            var passwordHash = _passwordHasherAdapter.Hash(request.Password);

            var roleResponse = await _roleService.GetByIdOrDefault(request.RoleId, cancellationToken);

            if (!roleResponse.IsSuccess)
            {
                _logger.LogWarning("Role with id {roleId} not found in database!", request.RoleId);

                return Result<AuthTokensResponse>
                    .Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Role not found!");
            }

            var user = User.Create(request.Email, passwordHash, roleResponse.Data!.Id);

            var apiResponse = await _userServiceApiRefitAdapter.CreateUserAsync(Mapper.Map(request));

            if (!apiResponse.IsSuccess)
            {
                _logger.LogError("Occurred an error in register route of the User Service!");

                return Result<AuthTokensResponse>
                    .Failure(apiResponse.StatusCode, apiResponse.Message, apiResponse.Errors.ToArray());
            }

            await _userService.CreateAsync(user, cancellationToken);

            var accessToken = await _jwtService.GenerateAccessToken(user.Id, roleResponse.Data.Name);

            var token = new Token(user.Id, DateTime.UtcNow.AddDays(7), ETokenType.RefreshToken);

            var refreshToken = (await _tokenService.CreateAsync(token, cancellationToken)).Data?.Value;

            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("User with email {email} registered sucessfully in database", request.Email);

            return Result<AuthTokensResponse>
                .Success(
                    Convert.ToInt32(HttpStatusCode.Created),
                    new AuthTokensResponse(accessToken, refreshToken!),
                    "User registered sucessfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred an error when trying to register an user. Error: {message}", ex.Message);

            throw;
        }
    }

    public async Task<Result<AuthTokensResponse>> SignInAsync(
        SignInUserRequest signInUserRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting sign-in of the user with email {email}!", signInUserRequest.Email);

            var userResponse = await _userService
                .GetUserWithRoleByEmailAsync(signInUserRequest.Email, cancellationToken);

            if (!userResponse.IsSuccess)
            {
                _logger.LogWarning("Invalid email attempt for user with email {email}", signInUserRequest.Email);

                return Result<AuthTokensResponse>
                    .Failure(Convert.ToInt32(HttpStatusCode.BadRequest), "Email or Password invalid!");
            }

            var userData = userResponse.Data;

            var isPasswordValid = _passwordHasherAdapter.Verify(signInUserRequest.Password, userData?.PasswordHash!);

            if (!isPasswordValid)
            {
                _logger.LogWarning("Invalid password attempt for user with email {Email}", signInUserRequest.Email);

                return Result<AuthTokensResponse>
                    .Failure(Convert.ToInt32(HttpStatusCode.BadRequest), "Email or Password invalid!");
            }

            var accessToken = await _jwtService.GenerateAccessToken(userData!.Id, userData.Role!.Name);

            var refreshToken = (await _tokenService
                .CreateAsync(
                    new Token(userData.Id,
                        DateTime.UtcNow.AddDays(7),
                        ETokenType.RefreshToken), cancellationToken)).Data?.Value;

            _logger.LogInformation("User with email {email} logged sucessfully!", signInUserRequest.Email);

            return Result<AuthTokensResponse>
                .Success(
                    Convert.ToInt32(HttpStatusCode.OK),
                    new AuthTokensResponse(accessToken, refreshToken!),
                    "User successfully signed in!");
        }
        catch (Exception ex)
        {
            _logger.LogError("Occurred an error when trying to sign-in an user. Error: {message}", ex.Message);

            throw;
        }
    }
}