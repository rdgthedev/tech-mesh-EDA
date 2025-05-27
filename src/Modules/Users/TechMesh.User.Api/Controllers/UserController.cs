using Microsoft.AspNetCore.Mvc;
using TechMesh.Api.Controllers;
using TechMesh.Application.Results;
using TechMesh.User.Application.Command.User;

namespace TechMesh.User.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : BaseController
    {
        [HttpPost("users")]
        public async Task<ActionResult<Result<string>>> CreateUser(
            [FromBody] CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            var result = await DispatchAsync(command, cancellationToken);

            return CustomResponse(result);
        }
    }
}
