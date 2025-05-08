using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechMesh.Api.Controllers;
using TechMesh.Auth.Application.DTOs.Auth.Request;
using TechMesh.Auth.Application.Interfaces.Services.Infra;
using Results = TechMesh.Application.Results.Results;

namespace TechMesh.Auth.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController : BaseController
{
    private readonly IAuthService _authService;

    protected AuthenticationController(
        IMediator mediator,
        IAuthService authService) : base(mediator) => _authService = authService;

    [HttpPost("sign-in")]
    public async Task<ActionResult<Results>> SignIn(
        [FromBody] SignInUserRequest signInUserRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _authService.SignInAsync(signInUserRequest, cancellationToken);

            return response;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}