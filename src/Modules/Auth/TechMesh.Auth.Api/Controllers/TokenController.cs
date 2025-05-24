namespace TechMesh.Auth.Api.Controllers;

[ApiController]
[Route("api/v1/tokens")]
public class TokenController : BaseController
{
    private readonly ITokenService _tokenService;

    protected TokenController(ITokenService tokenService) => _tokenService = tokenService;

    [HttpDelete("revoke/{token}")]
    public async Task<ActionResult<Result<bool>>> Revoke(
        [FromRoute] string token,
        CancellationToken cancellationToken)
    {
        return CustomResponse(await _tokenService.DeleteAsync(token, cancellationToken));
    }
}