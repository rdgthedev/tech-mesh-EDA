using TechMesh.Auth.Application.Interfaces.Services;
using TechMesh.Auth.Application.Interfaces.Services.Auth;

namespace TechMesh.Auth.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController : BaseController
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService) => _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<Result<AuthTokensResponse>>> Register(
        [FromBody] RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken)
        => CustomResponse(await _authService.RegisterAsync(registerUserRequest, cancellationToken));


    [HttpPost("sign-in")]
    public async Task<ActionResult<Result<AuthTokensResponse>>> SignIn(
        [FromBody] SignInUserRequest signInUserRequest,
        CancellationToken cancellationToken)
        => CustomResponse(await _authService.SignInAsync(signInUserRequest, cancellationToken));
}