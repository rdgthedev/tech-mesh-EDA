using TechMesh.Application.Results;
using TechMesh.Auth.Application.Interfaces.Services;
using TechMesh.Auth.Application.Interfaces.Services.Infra;
using TechMesh.Auth.Domain.Interfaces.Repositories;

namespace TechMesh.Auth.Infrastructure.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly ITokenService _tokenService;
    private readonly IRoleService _roleService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService,
        ITokenService tokenService,
        IRoleService roleService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _tokenService = tokenService;
        _roleService = roleService;
        _unitOfWork = unitOfWork;
    }

    // public async Task<Result<AuthTokensResponse>> RegisterAsync(
    //     RegisterUserRequest registerUserRequest,
    //     CancellationToken cancellationToken)
    // {
    //     var userExists = await _userService
    //         .ExistsAsync(u => u.Email == registerUserRequest.Email, cancellationToken);
    //
    //     if (userExists.Data)
    //         return Result<AuthTokensResponse>.Failure((int)HttpStatusCode.NotFound, "User already exists!");
    //
    //     var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserRequest.Password);
    //
    //     var role = registerUserRequest.RoleId != null && registerUserRequest.RoleId != Guid.Empty
    //         ? (await _roleService.GetByIdAsync(registerUserRequest.RoleId.Value, cancellationToken)).Data
    //         : (await _roleService.GetByNameAsync(nameof(User), cancellationToken)).Data;
    //
    //     if (role is null)
    //         return Result<AuthTokensResponse>.Failure((int)HttpStatusCode.NotFound, "Role not found!");
    //
    //     var newUser = User.Create(registerUserRequest.Email, passwordHash, role.Id);
    //
    //     //chamar endpoint /api/users (POST) -> User Service
    //
    //     var accessToken = _jwtService.GenerateAccessToken(newUser.Id, role.Name);
    //     var refreshToken = _jwtService.GenerateRefreshToken(newUser.Id, DateTime.Now.AddDays(7));
    //
    //     await _tokenService.CreateAsync(refreshToken, cancellationToken);
    //
    //     await _userService.CreateAsync(newUser, cancellationToken);
    //
    //     await _unitOfWork.CommitAsync(cancellationToken);
    //
    //     var authTokens = new AuthTokensResponse(accessToken, refreshToken.Value.ToString());
    //
    //     return Result<AuthTokensResponse>
    //         .Success((int)HttpStatusCode.Created, authTokens, "User registered with success!");
    // }

    public async Task<Results> SignInAsync(
        SignInUserRequest signInUserRequest,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithRoleByEmailAsync(signInUserRequest.Email, cancellationToken);

        if (user is null)
            return Results.Failure((int)HttpStatusCode.BadRequest, "Email or Password invalid!");

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(signInUserRequest.Password, user.PasswordHash);

        if (!isPasswordValid)
            return Results.Failure((int)HttpStatusCode.BadRequest, "Email or Password invalid!");

        var accessToken = _jwtService.GenerateAccessToken(user.Id, user.Role.Name);

        var refreshToken = _jwtService.GenerateRefreshToken(user.Id, DateTime.Now.AddDays(7));

        return Results.Success(new AuthTokensResponse(
                accessToken,
                refreshToken.Value.ToString()),
            "User successfully signed in!");
    }
}