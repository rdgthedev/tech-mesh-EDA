using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechMesh.Api.Controllers;
using TechMesh.Auth.Application.Interfaces.Services.Infra;
using Results = TechMesh.Application.Results.Results;

namespace TechMesh.Auth.Api.Controllers;

[ApiController]
[Route("api/v1/tokens")]
public class TokenController : BaseController
{
    private readonly ITokenService _tokenService;

    protected TokenController(
        IMediator mediator,
        ITokenService tokenService) : base(mediator) => _tokenService = tokenService;


    [HttpDelete("revoke/{token}")]
    public async Task<ActionResult<Results>> Revoke(
        [FromRoute] string token,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _tokenService.DeleteAsync(token, cancellationToken);

            return response;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}